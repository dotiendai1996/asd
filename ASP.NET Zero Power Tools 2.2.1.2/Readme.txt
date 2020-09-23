Configuration Instruction (MUST DONE PROPERLY BEFORE USING THE EXTENSION):

Change config.json under your <PROJECT>\aspnet-core\AspNetZeroRadTool folder, so the configuration is per project.

Example:

{
"CompanyName": "MyCompanyName", - Same as your project settings
"ProjectName": "AbpZeroTemplate", - Same as your project settings
"ProjectType": "ProjectType", - Two Option: Mvc / Angular (Case sensitive).
"ProjectVersion":"ProjectVersion", - Same as your project version, format example: v8.6.0
"ApplicationAreaName": "AppAreaName", - Same as your project settings.
"LicenseCode": "LicenseCodePlaceHolderToReplace", - For my patch, it can be deleted.
"AngularSrcPath": "\\..\\..\\angular\\src\\", - If you changed the location, it should change as well.
"AngularMergedSrcPath": "\\..\\src\\{{Namespace_Here}}.Web.Host\\src\\", - If you changed the location, it should change as well.
"CoreSrcPath": "\\..\\src\\", - If you changed the location, it should change as well.
"FileLocations":{
"DbContext" : "{{Namespace_Here}}.EntityFrameworkCore\\EntityFrameworkCore\\{{Project_Name_Here}}DbContext.cs",
"CustomDtoMapper" : "{{Namespace_Here}}.Application\\CustomDtoMapper.cs",
"AppAuthorizationProvider" : "{{Namespace_Here}}.Core\\Authorization\\AppAuthorizationProvider.cs",
"EntityHistoryHelper" : "{{Namespace_Here}}.Core\\EntityHistory\\EntityHistoryHelper.cs",
"AppPermissions" : "{{Namespace_Here}}.Core\\Authorization\\AppPermissions.cs",
"LocalizationFile" : "{{Namespace_Here}}.Core\\Localization\\{{Project_Name_Here}}\\{{Project_Name_Here}}.xml",
"EntityFrameWorkProjectFolder" : "{{Namespace_Here}}.EntityFrameworkCore",
"Mvc":{
"AppNavigationProvider" : "{{Namespace_Here}}.Web.Mvc\\Areas\\{{App_Area_Name_Here}}\\Startup\\{{App_Area_Name_Here}}NavigationProvider.cs",
"AppPageNames" : "{{Namespace_Here}}.Web.Mvc\\Areas\\{{App_Area_Name_Here}}\\Startup\\{{App_Area_Name_Here}}PageNames.cs",
"BundleConfig" : "{{Namespace_Here}}.Web.Mvc\\bundles.json"
},
"Angular":{
"AppNavigationService" : "app\\shared\\layout\\nav\\app-navigation.service.ts",
"ServiceProxies" : "shared\\service-proxies\\service-proxy.module.ts",
"Module" : "app\\{{menu_Position_Here}}\\{{menu_Position_Here}}.module.ts",
"RoutingModule" : "app\\{{menu_Position_Here}}\\{{menu_Position_Here}}-routing.module.ts"
}
}
}

The rest is newly added in 7.1.0 package, so I thought it can remain the same.

Don't forget to disable the auto plugin update to prevent it from suddenly not working.

I add some change to about form. If someday you find it not working, check the about form first.

Hajuuk

WWW.DOWNLOADLY.IR