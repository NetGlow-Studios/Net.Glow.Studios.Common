namespace Ngs.Common.Tools.Web.Tests;

public class BeautifiersTests
{
    [Fact]
    public void JsonBeautifier()
    {
        const string value = "[{\"Name\":\"Dawid\",\"Surname\":\"Mika\"}," +
                             "{\"Name\":\"Janek\",\"Surname\":\"Janosik\"}," +
                             "{\"Name\":\"Janusz\",\"Surname\":\"Suchy\"}]";

        const string expected = "[\r\n  {\r\n    \"Name\": \"Dawid\",\r\n    \"Surname\": \"Mika\"\r\n  }," +
                                "\r\n  {\r\n    \"Name\": \"Janek\",\r\n    \"Surname\": \"Janosik\"\r\n  }," +
                                "\r\n  {\r\n    \"Name\": \"Janusz\",\r\n    \"Surname\": \"Suchy\"\r\n  }\r\n]";
        
        var result = Web.Beautifiers.JsonBeautifier.Beautify(value);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void JavaScriptBeautifier()
    {
        // const string value = "function greet(name) {var message = 'Hello, ' + name + '!';console.log(message);}var person = 'Alice';greet(person);";
        //
        // const string expected = "function greet(name) {\n    var message = 'Hello, ' + name + '!';\n    console.log(message);\n}\n\nvar person = 'Alice';\ngreet(person);";
        //
        // var result = Web.Beautifiers.JavaScriptBeautifier.Beautify(value);
        //
        // Assert.Equal(expected, result);
    }
}