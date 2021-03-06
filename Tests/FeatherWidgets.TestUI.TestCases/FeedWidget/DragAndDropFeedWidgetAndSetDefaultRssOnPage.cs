﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Feather.Widgets.TestUI.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeatherWidgets.TestUI.TestCases.FeedWidget
{
    /// <summary>
    /// DragAndDropFeedWidgetAndSetDefaultRssOnPage test class.
    /// </summary>
    [TestClass]
    public class DragAndDropFeedWidgetAndSetDefaultRssOnPage_ : FeatherTestCase
    {
        /// <summary>
        /// UI test DragAndDropFeedWidgetAndSetDefaultRssOnPage
        /// </summary>
        [TestMethod,
        Owner(FeatherTeams.SitefinityTeam4),
        TestCategory(FeatherTestCategories.Bootstrap),
        TestCategory(FeatherTestCategories.PagesAndContent),
        TestCategory(FeatherTestCategories.Feed)]
        public void DragAndDropFeedWidgetAndSetDefaultRssOnPage()
        {
            BAT.Macros().NavigateTo().Modules().Forums();
            var forumsWrapper = BAT.Wrappers().Backend().Forums().ForumsWrapper();
            forumsWrapper.StartNewForumCreation();
            var createForumWrapper = BAT.Wrappers().Backend().Forums().CreateForumWrapper();
            createForumWrapper.SetForumTitle(forumTitle);
            createForumWrapper.SetForumDefaultPage(PageName);
            createForumWrapper.ClickCreateForumButton();

            BAT.Macros().NavigateTo().Pages(this.Culture);
            BAT.Wrappers().Backend().Pages().PagesWrapper().OpenPageZoneEditor(PageName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().AddWidgetToPlaceHolderPureMvcMode(WidgetName);
            BATFeather.Wrappers().Backend().Pages().PageZoneEditorWrapper().EditWidget(WidgetName);
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().ClickSelectButton();
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().SelectItemsInFlatSelector(forumTitle);
            BATFeather.Wrappers().Backend().Widgets().SelectorsWrapper().DoneSelecting();
            BATFeather.Wrappers().Backend().Widgets().WidgetDesignerWrapper().SaveChanges();
            BAT.Wrappers().Backend().Pages().PageZoneEditorWrapper().PublishPage();

            BAT.Macros().NavigateTo().CustomPage("~/" + PageName.ToLower(), false, this.Culture);
            BATFeather.Wrappers().Frontend().FeedWidget().FeedWidget().VerifyFeedLImageIsVisible();
            Assert.IsTrue(BAT.Wrappers().Frontend().Pages().PagesWrapperFrontend().GetPageContent().InnerText.Contains(forumTitle));
            Assert.IsTrue(ActiveBrowser.ContainsText(this.feedLink[0]), "Feed link not present");
            BATFeather.Wrappers().Frontend().FeedWidget().FeedWidget().VerifyFeedLinkInHeadTag(forumTitle, this.feedLink[0]);
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

        private const string PageName = "FeedPage";
        private const string WidgetName = "Feed";
        string forumTitle = "ForumTitle";
        private string[] feedLink = { "feeds/forumtitle" };
    }
}
