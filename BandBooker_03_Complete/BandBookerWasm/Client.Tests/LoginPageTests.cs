using System;
using System.Threading.Tasks;
using BandBookerWasm.Client.Pages;
using BandBookerWasm.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using VerifyBunit;
using Xunit;
using Xunit.Abstractions;

public class LoginPageTests :
    VerifyBase
{
    [Fact]
    public Task Simple()
    {
        var navigationMock = new Mock<NavigationManager>();
        var authMock = new Mock<IAuthService>();
        Services.Add(ServiceDescriptor.Singleton(provider => navigationMock.Object));
        Services.Add(ServiceDescriptor.Singleton(provider => authMock.Object));
        var component = RenderComponent<Login>();
        var model = component.Instance.LoginModel;
        model.Email = "a@b.com";
        model.Password = "thePassword";
        component.Render();
        return Verify(component);
    }

    public LoginPageTests(ITestOutputHelper output) :
        base(output)
    {
    }
}