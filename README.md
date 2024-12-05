1 Create project of type Blazor webassembly standalone

2 Install mandatory nugets
```
    <PackageReference Include="Fluxor.Blazor.Web" Version="5.9.1" />
    <PackageReference Include="Fluxor.Blazor.Web.ReduxDevTools" Version="5.9.1" />
```

3 Set up Fluxor in program.cs
```
    using Fluxor;
    using Fluxor.Blazor.Web.ReduxDevTools;

    builder.Services.AddFluxor(options =>
    {
        options.ScanAssemblies(typeof(Program).Assembly);
    
    #if DEBUG
        options.UseReduxDevTools();
    #endif
    });
```

4 Set up App.razor
```
<Fluxor.Blazor.Web.StoreInitializer/> //add this line

<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)"/>
        <FocusOnNavigate RouteData="@routeData" Selector="h1"/>
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="@typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```

5 create directory states with actions, effects , reducer and the state

6
