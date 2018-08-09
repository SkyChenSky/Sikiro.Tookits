# Sikiro.Tookits                                         [中文](https://github.com/SkyChenSky/Sikiro.Tookits/blob/master/README.zh-cn.md)
Sikiro.Tookits is base And Frequently-used Tools Library.

## Getting Started

### Nuget

You can run the following command to install the Sikiro.Tookits in your project。

```
PM> Install-Package Sikiro.Tookits
```

### What does it have？

* Base
```c#
var pl = new PageList<User>(1, 10, 100, new List<User>());

var sr = new ServiceResult<User>();
if (sr.Error)
    return;
```
* Extension
```c#
var list = new List<User>().DistinctBy(a => a.Name);

DataTable dt = list.ToDataTable();

int numString = "1".TryInt(1);
```
* Helper
```c#
Guid guid = GuidHelper.GenerateComb();
```
and so on

### Others
Besides Sikiro.Tookits, there are [Sikiro.Tookits.Files](https://github.com/SkyChenSky/Sikiro.Tookits.Files)、 [Sikiro.Tookits.LocalCache](https://github.com/SkyChenSky/Sikiro.Tookits.LocalCache) and [Sikiro.DapperLambdaExtension.MsSql](https://github.com/SkyChenSky/Sikiro.DapperLambdaExtension.MsSql)

## End
If you have good suggestions, please feel free to mention to me.

