#pragma checksum "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Areas\AdminArea\Views\Shared\_ValidationPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c381b25f7ba5f72b625355621f9855514532bc3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminArea_Views_Shared__ValidationPartial), @"mvc.1.0.view", @"/Areas/AdminArea/Views/Shared/_ValidationPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Areas\AdminArea\Views\_ViewImports.cshtml"
using BakuSpirtis;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Areas\AdminArea\Views\_ViewImports.cshtml"
using BakuSpirtis.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Areas\AdminArea\Views\_ViewImports.cshtml"
using BakuSpirtis.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Areas\AdminArea\Views\_ViewImports.cshtml"
using BakuSpirtis.ViewModels.Admin;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Areas\AdminArea\Views\_ViewImports.cshtml"
using BakuSpirtis.Utilities.Pagination;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c381b25f7ba5f72b625355621f9855514532bc3", @"/Areas/AdminArea/Views/Shared/_ValidationPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3228e78e43b96a8ea79b3027aedfa0b040557846", @"/Areas/AdminArea/Views/_ViewImports.cshtml")]
    public class Areas_AdminArea_Views_Shared__ValidationPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery-validate/1.19.3/jquery.validate.min.js""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/jquery-validation-unobtrusive/3.2.12/jquery.validate.unobtrusive.min.js""></script>
<publishData><publishProfile profileName=""Bakuspirits - Web Deploy"" publishMethod=""MSDeploy"" publishUrl=""bakuspirits.scm.azurewebsites.net:443"" msdeploySite=""Bakuspirits"" userName=""$Bakuspirits"" userPWD=""9WNBcopEiBqJDib8Lir3BuezfpFTqWKw4ECmitwghvcGdbXxRwsKAK4dLcxP"" destinationAppUrl=""http://bakuspirits.azurewebsites.net""");
            BeginWriteAttribute("SQLServerDBConnectionString", " SQLServerDBConnectionString=\"", 568, "\"", 598, 0);
            EndWriteAttribute();
            BeginWriteAttribute("mySQLDBConnectionString", " mySQLDBConnectionString=\"", 599, "\"", 625, 0);
            EndWriteAttribute();
            BeginWriteAttribute("hostingProviderForumLink", " hostingProviderForumLink=\"", 626, "\"", 653, 0);
            EndWriteAttribute();
            WriteLiteral(@" controlPanelLink=""http://windows.azure.com"" webSystem=""WebSites""><databases /></publishProfile><publishProfile profileName=""Bakuspirits - FTP"" publishMethod=""FTP"" publishUrl=""ftp://waws-prod-dm1-223.ftp.azurewebsites.windows.net/site/wwwroot"" ftpPassiveMode=""True"" userName=""Bakuspirits\$Bakuspirits"" userPWD=""9WNBcopEiBqJDib8Lir3BuezfpFTqWKw4ECmitwghvcGdbXxRwsKAK4dLcxP"" destinationAppUrl=""http://bakuspirits.azurewebsites.net""");
            BeginWriteAttribute("SQLServerDBConnectionString", " SQLServerDBConnectionString=\"", 1083, "\"", 1113, 0);
            EndWriteAttribute();
            BeginWriteAttribute("mySQLDBConnectionString", " mySQLDBConnectionString=\"", 1114, "\"", 1140, 0);
            EndWriteAttribute();
            BeginWriteAttribute("hostingProviderForumLink", " hostingProviderForumLink=\"", 1141, "\"", 1168, 0);
            EndWriteAttribute();
            WriteLiteral(@" controlPanelLink=""http://windows.azure.com"" webSystem=""WebSites""><databases /></publishProfile><publishProfile profileName=""Bakuspirits - Zip Deploy"" publishMethod=""ZipDeploy"" publishUrl=""bakuspirits.scm.azurewebsites.net:443"" userName=""$Bakuspirits"" userPWD=""9WNBcopEiBqJDib8Lir3BuezfpFTqWKw4ECmitwghvcGdbXxRwsKAK4dLcxP"" destinationAppUrl=""http://bakuspirits.azurewebsites.net""");
            BeginWriteAttribute("SQLServerDBConnectionString", " SQLServerDBConnectionString=\"", 1548, "\"", 1578, 0);
            EndWriteAttribute();
            BeginWriteAttribute("mySQLDBConnectionString", " mySQLDBConnectionString=\"", 1579, "\"", 1605, 0);
            EndWriteAttribute();
            BeginWriteAttribute("hostingProviderForumLink", " hostingProviderForumLink=\"", 1606, "\"", 1633, 0);
            EndWriteAttribute();
            WriteLiteral(" controlPanelLink=\"http://windows.azure.com\" webSystem=\"WebSites\"><databases /></publishProfile></publishData>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591