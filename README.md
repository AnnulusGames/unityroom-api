# Unityroom API
 Unofficial API client for unityroom

Unityroom APIは、UnityからunityroomのAPIを利用するためのライブラリです。このリポジトリは[unityroom-client-library](https://github.com/naichilab/unityroom-client-library)のForkであり、よりシンプルで扱いやすいC# APIの提供を目標としています。

## セットアップ

### 要件

* Unity 2020.3(LTS)以上

### インストール

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力し、Addを押す
```
https://github.com/AnnulusGames/unityroom-api.git?path=Assets/Unityroom
```
あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記
```
{
    "dependencies": {
        "com.annulusgames.unityroom-api": "https://github.com/AnnulusGames/unityroom-api.git?path=Assets/Unityroom"
    }
}
```

## ランキングの実装

### 設定

1. [こちら](https://unityroom-help.notion.site/4fae458305a948818b90e50dcad6a3f3?pvs=4)の手順に従い、unityroomでの準備を行う
2. APIキー画面にてHMAC認証用キーをコピー
3. Project Settings > Unityroomを開き、Create Settings Assetを押して設定ファイルを作成
4. HMAC Keyにunityroom側で取得したHMAC認証用キーを入力

### C# API

`UnityroomAPI.ReportScore()`からスコアの送信が可能です。

```cs
using UnityEngine;
using Unityroom; // C# Scriptの冒頭にusingを追記

// スコアを送信する
UnityroomAPI.ReportScore(1, 123.45f);

// コールバックを設定することも可能
UnityroomAPI.ReportScore(1, 123.45f, (isSuccess) =>
{
    if (isSuccess)
    {
        // 成功時の処理
        Debug.Log("Success");
    }
    else
    {
        // 失敗時の処理
        Debug.Log("Failed");
    }
});
```

### 注意点

Create Settings Assetで作成したUnityroomSettings.assetファイルを移動させる場合、必ずResourcesフォルダ内に含めるようにしてください。

# ライセンス

[MIT Lisence](LICENSE)
