# treeHolesApi

这个项目是一个学习项目，主要实现一个树洞的功能，但目前有一个`Error`,无法使用`DbContext`，有谁知道原因嘛？

目前报错信息：`Method not found: 'System.String Microsoft.EntityFrameworkCore.Diagnostics.RelationalStrings.get_NoConnectionOrConnectionString()'.`

完整报错信息：
```shell

System.MissingMethodException: Method not found: 'System.String Microsoft.EntityFrameworkCore.Diagnostics.RelationalStrings.get_NoConnectionOrConnectionString()'.
   at MySql.EntityFrameworkCore.Internal.MySQLOptions.GetConnectionSettings(MySQLOptionsExtension relationalOptions)
   at MySql.EntityFrameworkCore.Internal.MySQLOptions.Initialize(IDbContextOptions options)
   at Microsoft.EntityFrameworkCore.Internal.SingletonOptionsInitializer.EnsureInitialized(IServiceProvider serviceProvider, IDbContextOptions options)
   at Microsoft.EntityFrameworkCore.Internal.ServiceProviderCache.<GetOrAdd>g__BuildServiceProvider|4_1(IDbContextOptions _, ValueTuple`2 arguments)
   at Microsoft.EntityFrameworkCore.Internal.ServiceProviderCache.<>c.<GetOrAdd>b__4_0(IDbContextOptions contextOptions, ValueTuple`2 tuples)
   at System.Collections.Concurrent.ConcurrentDictionary`2.GetOrAdd[TArg](TKey key, Func`3 valueFactory, TArg factoryArgument)
   at Microsoft.EntityFrameworkCore.Internal.ServiceProviderCache.GetOrAdd(IDbContextOptions options, Boolean providerRequired)
   at Microsoft.EntityFrameworkCore.DbContext..ctor(DbContextOptions options)
   at treeHolesApi.Services.TreeDbContext..ctor(DbContextOptions`1 option) in G:\树洞\treeHolesApi\treeHolesApi\Services\TreeDbContext.cs:line 10
   at System.RuntimeMethodHandle.InvokeMethod(Object target, Span`1& arguments, Signature sig, Boolean constructor, Boolean wrapExceptions)
   at System.Reflection.RuntimeConstructorInfo.Invoke(BindingFlags invokeAttr, Binder binder, Object[] parameters, CultureInfo culture)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitConstructor(ConstructorCallSite constructorCallSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitCache(ServiceCallSite callSite, RuntimeResolverContext context, ServiceProviderEngineScope serviceProviderEngine, RuntimeResolverLock lockType)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.VisitScopeCache(ServiceCallSite callSite, RuntimeResolverContext context)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteVisitor`2.VisitCallSite(ServiceCallSite callSite, TArgument argument)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteRuntimeResolver.Resolve(ServiceCallSite callSite, ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.DynamicServiceProviderEngine.<>c__DisplayClass2_0.<RealizeService>b__0(ServiceProviderEngineScope scope)
   at Microsoft.Extensions.DependencyInjection.ServiceProvider.GetService(Type serviceType, ServiceProviderEngineScope serviceProviderEngineScope)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope.GetService(Type serviceType)
   at Microsoft.Extensions.DependencyInjection.ActivatorUtilities.GetService(IServiceProvider sp, Type type, Type requiredBy, Boolean isDefaultParameterRequired)
   at lambda_method5(Closure , IServiceProvider , Object[] )
   at Microsoft.AspNetCore.Mvc.Controllers.ControllerActivatorProvider.<>c__DisplayClass7_0.<CreateActivator>b__0(ControllerContext controllerContext)
   at Microsoft.AspNetCore.Mvc.Controllers.ControllerFactoryProvider.<>c__DisplayClass6_0.<CreateControllerFactory>g__CreateController|0(ControllerContext controllerContext)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|26_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ExceptionContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeFilterPipelineAsync()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)

HEADERS
=======
Accept: text/plain
Host: localhost:5245
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/107.0.0.0 Safari/537.36 Edg/107.0.1418.42
:method: GET
Accept-Encoding: gzip, deflate, br
Accept-Language: zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6
Origin: http://localhost:5244
Referer: http://localhost:5244/
sec-ch-ua: "Microsoft Edge";v="107", "Chromium";v="107", "Not=A?Brand";v="24"
DNT: 1
sec-ch-ua-mobile: ?0
sec-ch-ua-platform: "Windows"
sec-fetch-site: cross-site
sec-fetch-mode: cors
sec-fetch-dest: empty

```

## 替代方案

我觉得这可能是`Nuget`包的原因，没有合适和包支持`EF Core 7`,所以我对涉及到的包都进行了降级，目前勉强可用了，但理论上这个问题并没有解决。