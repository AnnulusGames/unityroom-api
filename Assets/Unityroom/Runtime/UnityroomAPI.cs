using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;
using Unityroom.Internal;

namespace Unityroom
{
    public static class UnityroomAPI
    {
        const string MESSAGE_REPORT_SCORE_ON_EDITOR = "[unityroom] Editor上でのReportScoreは常に失敗します。unityroomにゲームをアップロードすると正しく送信されます。";
        const string MESSAGE_SETTINGS_ASSET_NOT_FOUND = "[unityroom] 設定ファイルが見つかりません。Project Settings > Untiyroom から設定ファイルのアセットを作成してください。";

        public static void ReportScore(int scoreboardId, float value, Action<bool> callback = null)
        {
            if (UnityroomSettings.Instance == null)
            {
                Debug.LogError(MESSAGE_SETTINGS_ASSET_NOT_FOUND);
                return;
            }

#if UNITY_EDITOR
            Debug.LogWarning(MESSAGE_REPORT_SCORE_ON_EDITOR);
            callback?.Invoke(false);
#else
            CoroutineDispatcher.Instance.Run(ReportScoreEnumerator(scoreboardId, value, callback));
#endif
        }

        private static IEnumerator ReportScoreEnumerator(int scoreboardId, float value, Action<bool> callback)
        {
            // スコア送信APIエンドポイント
            var path = $"/gameplay_api/v1/scoreboards/{scoreboardId}/scores";

            // 現在のUNIX TIMEを取得
            var unixTime = UnixTime.GetCurrentUnixTime()
                .ToString();

            // 送信するスコアを文字列に変換しておく
            var scoreText = value.ToString(CultureInfo.InvariantCulture);

            // 認証用のHMAC(Hash-based Message Authentication Code)を計算する
            var hmacDataText = $"POST\n{path}\n{unixTime}\n{scoreText}";
            var hmac = Hmac.GetHmacSha256(hmacDataText, UnityroomSettings.Instance.HmacKey);

            // APIリクエストを送信する
            // スコアはFormDataとして付与する
            // 認証用の情報はリクエストヘッダーに付与する
            var form = new WWWForm();
            form.AddField("score", scoreText);
            using var request = UnityWebRequest.Post(path, form);
            request.SetRequestHeader("X-Unityroom-Signature", hmac);
            request.SetRequestHeader("X-Unityroom-Timestamp", unixTime);
            yield return request.SendWebRequest();

            callback?.Invoke(request.result == UnityWebRequest.Result.Success);
        }
    }
}
