#pragma checksum "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\Contact\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dade6e858797f53feedfc05f3b18ceeea678759d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Contact_Index), @"mvc.1.0.view", @"/Views/Contact/Index.cshtml")]
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
#line 1 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\_ViewImports.cshtml"
using BakuSpirtis;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\_ViewImports.cshtml"
using BakuSpirtis.Utilities.Pagination;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\_ViewImports.cshtml"
using BakuSpirtis.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\_ViewImports.cshtml"
using BakuSpirtis.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\_ViewImports.cshtml"
using BakuSpirtis.ViewModels.Account;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\_ViewImports.cshtml"
using BakuSpirtis.ViewModels.Admin;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dade6e858797f53feedfc05f3b18ceeea678759d", @"/Views/Contact/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ca1bf4c82c1692f283160fdba28652e640bbe1a", @"/Views/_ViewImports.cshtml")]
    public class Views_Contact_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Dictionary<string, string>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/css/contact.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\Contact\Index.cshtml"
  
    ViewData["Title"] = "Əlaqə";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            DefineSection("Css", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "dade6e858797f53feedfc05f3b18ceeea678759d4975", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            WriteLiteral(@"<main>
    <section id=""contact-area"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                    <div class=""message-area text-center"">
                        <h1>Birbaşa Əlaqə</h1>
                        <ul class=""list-group mt-3"">

                            <li class=""list-group"">
                                <p><i class=""fal fa-envelope  fa-xl""></i> ");
#nullable restore
#line 19 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\Contact\Index.cshtml"
                                                                     Write(Model["Email"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </li>\r\n\r\n                            <li class=\"list-group\">\r\n                                <p><i class=\"fal fa-phone fa-xl\"></i> ");
#nullable restore
#line 23 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\Contact\Index.cshtml"
                                                                 Write(Model["Phone"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            </li>\r\n                            <li class=\"list-group\">\r\n                                <p>\r\n                                    <i class=\"fal fa-map-marker-alt  fa-xl\"></i> ");
#nullable restore
#line 27 "C:\Users\Lenovo\Desktop\BakuSpirits\BakuSpirtis\BakuSpirtis\Views\Contact\Index.cshtml"
                                                                            Write(Model["Address"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                </p>
                            </li>

                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <div class=""location-area"">
            <div class=""row mt-3"">
                <div class=""col-xl-12 col-lg-12 col-md-12 col-sm-12 col-xs-12"">
                    <div class=""location bg-dark"">
                        <iframe src=""https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3036.7014632082505!2d49.98610331523253!3d40.43760897936269!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x0%3A0x7f34d93dc538e2f5!2zNDDCsDI2JzE1LjQiTiA0OcKwNTknMTcuOSJF!5e0!3m2!1sen!2s!4v1652705542843!5m2!1sen!2s"" width=""1540"" height=""600"" style=""border:0;""");
            BeginWriteAttribute("allowfullscreen", " allowfullscreen=\"", 1858, "\"", 1876, 0);
            EndWriteAttribute();
            WriteLiteral(" loading=\"lazy\" referrerpolicy=\"no-referrer-when-downgrade\"></iframe>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n</main>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Dictionary<string, string>> Html { get; private set; }
    }
}
#pragma warning restore 1591
