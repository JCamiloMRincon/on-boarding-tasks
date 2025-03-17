using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace tests.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly ILocator _usernameInput;
        private readonly ILocator _passwordInput;
        private readonly ILocator _loginButton;
        private readonly ILocator _errorMessage;

        public LoginPage(IPage page) 
        {
            _page = page;
            _usernameInput = page.Locator("#user-name");
            _passwordInput = page.Locator("#password");
            _loginButton = page.Locator("#login-button");
            _errorMessage = page.Locator("[data-test='error']");
        }

        public async Task FillLoginForm(string user, string password) 
        {
            await _usernameInput.WaitForAsync();
            await _usernameInput.FillAsync(user);
            
            await _passwordInput.WaitForAsync();
            await _passwordInput.FillAsync(password);
            
            await _loginButton.ClickAsync();
        }

        public async Task<string> GetErrorMessageAfterLogin() 
        {
            await _errorMessage.WaitForAsync();
            return await _errorMessage.InnerTextAsync();
        }
    }
}
