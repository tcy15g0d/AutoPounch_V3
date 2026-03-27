# AutoPounch_V3

## 版本
v3.1.0

## 說明

1. 此程式是透過 iCloud 的「打卡系統連結」登入打卡系統。

2. 記得在執行檔（.exe）相同路徑下放置有帳號密碼的 `info.json`，可以一次輸入多個帳號，程式執行時會一同打卡。

3. 主程式執行時需要輸入參數來判斷簽到或簽退，可使用 bat 檔執行：

   | 檔案 | 說明 |
   |---|---|
   | `PouchIn.bat` | 簽到 |
   | `PouchOut.bat` | 簽退 |
   | `BotListener.bat` | 啟動 Telegram Bot 監聽（初次設定用） |

4. 要有 Windows 內建的工作排程器才能達成定時打卡的功能。

## Telegram 通知設定

1. 至 Telegram 搜尋 `@BotFather`，建立一個 Bot 並取得 **Bot Token**。

2. 將 Token 填入 `info.json` 的 `telegram_token` 欄位。

3. 執行 `BotListener.bat` 啟動監聽。

4. 每位使用者對 Bot 傳送 `/chatid`，Bot 會回覆各自的 Chat ID。

5. 將 Chat ID 填入 `info.json` 對應帳號的 `telegram` 欄位。

6. 按 `Ctrl+C` 關閉監聽，之後打卡時即會自動發送通知。

> `telegram` 欄位留空則不發送通知，不影響打卡功能。

## info.json 格式

```json
{
  "telegram_token": "Bot Token",
  "users": [
    {
      "id": "學號",
      "pw": "密碼",
      "line": "",
      "telegram": "Chat ID"
    }
  ]
}
```

## 版本紀錄

| 版本 | 日期 | 說明 |
|---|---|---|
| v3.1.0 | 2026-03-27 | 新增 Telegram Bot 打卡結果通知 |
| v3.0.0 | 2023-10-22 | 初始版本 |
