#pragma checksum "C:\Users\lenfa\Projects\ProjectManagmentApp\ProjectManagment.Services\ProjectManagment.Api\MailUtilities\Templates\ActivateAccount.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87043e77efac8ae7fee2c3381d8bfd9e90e49a4b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.MailUtilities_Templates_ActivateAccount), @"mvc.1.0.view", @"/MailUtilities/Templates/ActivateAccount.cshtml")]
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
#line 1 "C:\Users\lenfa\Projects\ProjectManagmentApp\ProjectManagment.Services\ProjectManagment.Api\MailUtilities\Templates\ActivateAccount.cshtml"
using ProjectManagment.Api.MailUtilities.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87043e77efac8ae7fee2c3381d8bfd9e90e49a4b", @"/MailUtilities/Templates/ActivateAccount.cshtml")]
    public class MailUtilities_Templates_ActivateAccount : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AccountActivationViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h2>Account activation</h2>\r\n<p>Hello ");
#nullable restore
#line 5 "C:\Users\lenfa\Projects\ProjectManagmentApp\ProjectManagment.Services\ProjectManagment.Api\MailUtilities\Templates\ActivateAccount.cshtml"
    Write(Model.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(",</p>\r\n<p>Click on this <a");
            BeginWriteAttribute("href", " href=\"", 170, "\"", 196, 1);
#nullable restore
#line 6 "C:\Users\lenfa\Projects\ProjectManagmentApp\ProjectManagment.Services\ProjectManagment.Api\MailUtilities\Templates\ActivateAccount.cshtml"
WriteAttributeValue("", 177, Model.ActivateLink, 177, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">link</a> to activation your account</p>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AccountActivationViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
