using Net.Glow.Studios.Common.Notify;
using Net.Glow.Studios.Common.Notify.Models;

namespace Ngs.Common.Notify.Tests;

public class MailServiceTest
{
    [Fact]
    public async Task SendEmailTestAsync()
    {
        //New mailBuilder instance
        var mailBuilder = new MailBuilder("To jest testowy email");

        //Receivers
        mailBuilder.AddTo("davex.pl122@gmail.com");
        //Create new bodyBuilder
        var body = new MailBodyBuilder(mailBuilder.GetSubject());

        //Configure style class
        body.AddClass("color-primary", new[] { "background-color:#DD554B" });

        //Add Hearders
        body.AddH1("To jest testowy nagłówek 1 stopnia", "color-primary");
        body.AddH2("To jest testowy nagłówek 2 stopnia", string.Empty, "color:white", "background-color:black");
        body.AddH3("To jest testowy nagłówek 3 stopnia", string.Empty, "color:lightblue", "background-color:black");
        body.AddH4("To jest testowy nagłówek 4 stopnia", string.Empty, "color:pink", "background-color:black");
        body.AddH5("To jest testowy nagłówek 5 stopnia", string.Empty, "color:green", "background-color:black");

        body.AddP("Uwu", string.Empty, "color:black");
        body.AddI("Uwu", "color:black");
        body.AddU("Uwu", "color:black");

        body.AddLink("Kliknij Mnie!", "www.youtube.com", string.Empty);

        //Add body to mail
        mailBuilder.AddBody(body);

        //Configure server credetials
        var mailService = new MailService(new MailConfiguration
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Email = "netglow.studios@gmail.com",
            Token = "oyytoantjkkquayx",
            EnableSsl = true
        });

        var mail = mailBuilder.Build();

        await mailService.SendMailAsync(mail);
    }
}