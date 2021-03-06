﻿using System;
using System.Web.Security;
using Telerik.Sitefinity.Frontend.Identity.Mvc.Models.Registration;

namespace FeatherWidgets.TestUnit.DummyClasses.Identity
{
    /// <summary>
    /// This class is used for testing the Registration controller.
    /// </summary>
    public class DummyRegistrationModel : IRegistrationModel
    {
        public Guid? LoginPageId { get; set; }

        public string CssClass { get; set; }

        public string MembershipProviderName { get; set; }

        public string SerializedSelectedRoles { get; set; }

        public SuccessfulRegistrationAction SuccessfulRegistrationAction { get; set; }

        public bool SendEmailOnSuccess { get; set; }

        public string SuccessEmailSubject { get; set; }

        public Guid? SuccessEmailTemplateId { get; set; }

        public string EmailSenderName { get; set; }

        public ActivationMethod ActivationMethod { get; set; }

        public Guid? SuccessfulRegistrationPageId { get; set; }

        public bool SendRegistrationEmail { get; set; }

        public string ConfirmationEmailSubject { get; set; }

        public Guid? ConfirmationEmailTemplateId { get; set; }

        public Guid? ConfirmationPageId { get; set; }

        public string DefaultReturnUrl { get; set; }

        public string ProfileBindings { get; set; }

        public bool EmailAddressShouldBeTheUsername { get; set; }

        public MembershipCreateStatus RegisterUser(RegistrationViewModel model)
        {
            if (model.Email != "fail@mail.com")
                return MembershipCreateStatus.Success;
            else
                return MembershipCreateStatus.InvalidUserName;
        }

        public string ErrorMessage(MembershipCreateStatus status)
        {
            return status.ToString();
        }

        public string GetPageUrl(Guid? pageId)
        {
            if (pageId.HasValue)
            {
                return "http://" + pageId.Value.ToString("D");
            }
            else
            {
                return null;
            }
        }

        public string GetError()
        {
            return null;
        }

        public bool ResendConfirmationEmail(string email)
        {
            return true;
        }

        public void InitializeViewModel(RegistrationViewModel viewModel)
        {
        }

        public string SerializedExternalProviders { get; set; }

        public void AuthenticateExternal(string input, System.Web.HttpContextBase context)
        {
            // throw new NotImplementedException();
        }
    }
}
