# wework-finance-sdk-csharp
企业微信 会话存档 sdk for csharp .NET 对接示例；

示例中.NET Core 3.1实现，需要在 vs 2019 或以上版本运行；



## 运行Sample

1、填入企业会话存档配置信息

```c#
var client = new FinanceSample("[企业Id]", "[企业会话存档Secret]");
var privateKey = @"[会话存档RSA私钥(xml格式)]";
```

2、配置拉取参数

```C#
var (data, seq) = client.GetChatData(
         new Dictionary<int, string> { { 1, privateKey } },
         0,
         10);
```

3、F5运行

