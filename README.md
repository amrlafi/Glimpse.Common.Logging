Glimpse.Common.Logging
======================

Common.Logging adapter for Glimpse


Installation
------------

Add/replace your adapter with Glimpse.Common.Logging.GlimpseLoggerFactoryAdapter

```xml
<common>
  <logging>
    <factoryAdapter type="Glimpse.Common.Logging.GlimpseLoggerFactoryAdapter, Glimpse.Common.Logging">
    </factoryAdapter>
  </logging>
</common>
```
Or consider MultipleLogger(https://www.nuget.org/packages/Common.Logging.MultipleLogger/) if you wish to use multiple adapters.

NuGet package: https://www.nuget.org/packages/Glimpse.Common.Logging/


Usage:

The library adds Common.Logging tab to Glimpse showing all loggers

![screen shot](http://i.imgur.com/leCSXYl.jpg "Common.Logging tab")
