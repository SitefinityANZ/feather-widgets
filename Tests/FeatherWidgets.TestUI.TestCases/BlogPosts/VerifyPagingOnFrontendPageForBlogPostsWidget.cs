﻿using System;
using ArtOfTest.WebAii.Core;
using Feather.Widgets.TestUI.Framework;
using Feather.Widgets.TestUI.Framework.Framework.Wrappers.Backend.Widgets;
using FeatherWidgets.TestUI.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.Sitefinity.TestUI.Framework.Utilities;

namespace FeatherWidgets.TestUI.TestCases.BlogPosts
{
    /// <summary>
    /// VerifyPagingOnFrontendPageForBlogPostsWidget_ test class.
    /// </summary>
    [TestClass]
    public class VerifyPagingOnFrontendPageForBlogPostsWidget_ : FeatherTestCase
    {
        /// <summary>
        /// UI test VerifyPagingOnFrontendPageForBlogPostsWidget
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam7),
        TestCategory(FeatherTestCategories.PagesAndContent), 
        TestCategory(FeatherTestCategories.Blogs)]
        public void VerifyPagingOnFrontendPageForBlogPostsWidget()
        {
            RuntimeSettingsModificator.ExecuteWithClientTimeout(800000, () => BAT.Macros().NavigateTo().CustomPage("~/sitefinity/pages", true, null, new HtmlFindExpression("class=~sfMain")));
            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ChangePagingOrLimitValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SwitchToListSettingsTab();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyCheckedRadioButtonOption(WidgetDesignerRadioButtonIds.UsePaging);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("2", "Paging");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().VerifyPageValue("20", "Limit");
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().PressCancelButton();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), true, this.Culture);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle7, PostTitle6 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle5, PostTitle4, PostTitle3, PostTitle2, PostTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("2", 4);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle5, PostTitle4 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle7, PostTitle6, PostTitle3, PostTitle2, PostTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("3", 4);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle3, PostTitle2 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle7, PostTitle6, PostTitle5, PostTitle4, PostTitle1 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("4", 4);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle1 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle7, PostTitle6, PostTitle5, PostTitle4, PostTitle3, PostTitle2 }));
            BATFeather.Wrappers().Frontend().CommonWrapper().NavigateToPageUsingPager("1", 4);
            Assert.IsTrue(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle7, PostTitle6 }));
            Assert.IsFalse(BATFeather.Wrappers().Frontend().Blogs().BlogsWrapper().IsBlogPostTitlesPresentOnThePageFrontend(new string[] { PostTitle5, PostTitle4, PostTitle3, PostTitle2, PostTitle1 }));
            BAT.Macros().NavigateTo().Pages(this.Culture);
        }

        /// <summary>
        /// Performs Server Setup and prepare the system with needed data.
        /// </summary>
        protected override void ServerSetup()
        {
            BAT.Macros().User().EnsureAdminLoggedIn();
            BAT.Arrange(this.TestName).ExecuteSetUp();
        }

        /// <summary>
        /// Performs clean up and clears all data created by the test.
        /// </summary>
        protected override void ServerCleanup()
        {
            BAT.Arrange(this.TestName).ExecuteTearDown();
        }

        private const string PageName = "BlogsPage";
        private const string WidgetName = "Blog posts";
        private const string PostTitle1 = "Post1";
        private const string PostTitle2 = "Post2";
        private const string PostTitle3 = "Post3";
        private const string PostTitle4 = "Post4";
        private const string PostTitle5 = "Post5";
        private const string PostTitle6 = "PostNew1";
        private const string PostTitle7 = "PostNew2";
    }
}
