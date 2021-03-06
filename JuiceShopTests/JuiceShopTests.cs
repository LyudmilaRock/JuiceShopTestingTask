using JuiceShopTests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

/// <summary>
/// Task: Selenium + PageObject
/// Create 2 tests.
/// https://juice-shop-ozar.herokuapp.com/
/// Juice Shop credentials:
/// admin@juice-sh.op
/// admin123
/// Test 1 - Enter invalid password and check that warning about invalid password appears
/// Test 2 - Log system in using specified login and password and make a purchase (Note: the cards are fake).
/// Check that card is empty after the purchase has been performed
/// Should be used:
/// - PageObject Pattern
/// - Implicit and Explicit waits
/// - If test failed - screenshot should be made
/// </summary>

namespace JuiceShopTests
    {
    [TestFixture]
    public class JuiceShopTests : BaseTestClass
    {
       
        [Test]
        public void InvalidLoginTest()
        {
            var landingPage = new LandingPagePageObject(webdriver);
            landingPage
                .DismissWelcomePopup();

            var headerFooter = new HeaderFooterPageObject(webdriver);
            headerFooter
                .SignIn();

            var loginPopup = new LoginPopupPageObject(webdriver);
            loginPopup
                .Login(TestSettings.validLogin, TestSettings.invalidPassword);

            Assert.IsTrue(loginPopup.CheckMessageAfterInvalidLogin("Invalid email or password."));

        }
        
        [Test]
        public void MakingPurchaseTest()
        {
            var landingPage = new LandingPagePageObject(webdriver);
            landingPage
                .DismissWelcomePopup();

            var headerFooter = new HeaderFooterPageObject(webdriver);
            headerFooter
                .SignIn();

            var loginPopup = new LoginPopupPageObject(webdriver);
            loginPopup
                .Login(TestSettings.validLogin, TestSettings.validPassword);

            
            var mainPage = new MainPagePageObject(webdriver);
            mainPage
                .BuyBananaJuice();
            mainPage
                .DismissCookies();
            mainPage
                .NavigateToNextPage();
            mainPage
                .ByMug();
            var shoppingCart = mainPage
                .OpenShoppingCart();
            shoppingCart
                .Chekout();
            shoppingCart
                .AddressSelection();
            shoppingCart
                .ContinueToDelivery();
            shoppingCart
                .FastDeliverySelection();
            shoppingCart
                .ContinueToPaimentOptions();
            shoppingCart
                .CardSelection();
            shoppingCart
                .ContinueToReview();
            shoppingCart
                .PayOrder();

            Assert.IsTrue(shoppingCart.CheckTextAfterPurchase("Thank you for your purchase!"));

            Assert.IsTrue(shoppingCart.CkeckBusketIsEmpty("0"));
                
        }
    }
}