#pragma checksum "D:\CRUD\CRUD Operations\CRUD.PL\Views\Shared\_Notifications.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "d57427134cdce5005486e05d6157c2530447328fd0d18b3b2d33d73b9371463a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Notifications), @"mvc.1.0.view", @"/Views/Shared/_Notifications.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\CRUD\CRUD Operations\CRUD.PL\Views\_ViewImports.cshtml"
using CRUD.PL;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\CRUD\CRUD Operations\CRUD.PL\Views\_ViewImports.cshtml"
using CRUD.DAL.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\CRUD\CRUD Operations\CRUD.PL\Views\_ViewImports.cshtml"
using CRUD.PL.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\CRUD\CRUD Operations\CRUD.PL\Views\_ViewImports.cshtml"
using CRUD.BLL.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\CRUD\CRUD Operations\CRUD.PL\Views\_ViewImports.cshtml"
using MediatR;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\CRUD\CRUD Operations\CRUD.PL\Views\_ViewImports.cshtml"
using CRUD.BLL.CQRS.DepartmentCqrs.Queries;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"d57427134cdce5005486e05d6157c2530447328fd0d18b3b2d33d73b9371463a", @"/Views/Shared/_Notifications.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"45d59d6c8c301614b0ae2dfb6c43f197c9d4f10398e1398059fd79ef70c64794", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__Notifications : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d57427134cdce5005486e05d6157c2530447328fd0d18b3b2d33d73b9371463a4251", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n<script src=\"//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js\"></script>\r\n");
#nullable restore
#line 3 "D:\CRUD\CRUD Operations\CRUD.PL\Views\Shared\_Notifications.cshtml"
 if (TempData["Success"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script type=\"text/javascript\">\r\n        toastr.success(\'");
#nullable restore
#line 6 "D:\CRUD\CRUD Operations\CRUD.PL\Views\Shared\_Notifications.cshtml"
                   Write(TempData["Success"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n");
            WriteLiteral("    </script>\r\n");
#nullable restore
#line 9 "D:\CRUD\CRUD Operations\CRUD.PL\Views\Shared\_Notifications.cshtml"
}
else if (TempData["Pdf"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script type=\"text/javascript\">\r\n        toastr.success(\'");
#nullable restore
#line 13 "D:\CRUD\CRUD Operations\CRUD.PL\Views\Shared\_Notifications.cshtml"
                   Write(TempData["Pdf"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\r\n    </script>\r\n");
#nullable restore
#line 15 "D:\CRUD\CRUD Operations\CRUD.PL\Views\Shared\_Notifications.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
