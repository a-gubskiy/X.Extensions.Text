# X.Text

## Overview
The `X.Text` library provides a set of utility classes designed to simplify common text processing tasks in .NET applications. It is ideal for developers looking to handle string manipulations, such as substring extraction, character cleaning, plain text conversion from HTML, and keyword extraction efficiently.

## Installation
To use the `X.Text` library in your project, include it as a dependency in your .NET project. You can install it from [NuGet](https://www.nuget.org/packages/X.Text/3.0.0#readme-body-tab).

## Usage

### `TextHelper`
Static methods contained within this class provide functionality for text manipulation without the need to instantiate any object.

**Features:**
- **Substring Extraction**: Get a substring from a text string optionally ending with a specified string if the text exceeds a certain length.
- **Character Cleaning**: Remove or replace system characters and excessive spaces from a text string.
- **HTML to Plain Text Conversion**: Convert HTML content to plain text, with options to preserve HTML line breaks.
- **Keyword Extraction**: Extract the most frequent keywords from a string, useful for generating metadata or summaries.

### `StringExtensions`
Extension methods for the `System.String` class leveraging the `TextHelper` utilities to extend string functionalities directly on string objects.

**Methods:**
- `Substring(this string text, int length, string endPart)`: Extension method to extract a substring.
- `CleanCharacters(this string text)`: Clean system-defined characters from the string.
- `Replace(this string text, IEnumerable<string> forReplace, string whichReplace)`: Replace specified substrings.
- `ToPlainText(this string text, bool saveHtmlLineBreaks)`: Convert HTML formatted string to plain text.
- `GetKeywords(this string text, int count)`: Extract keywords from text.

## Examples
```csharp
using X.Text.Extensions;

string exampleHtml = "<p>Hello World!</p>";
string plainText = exampleHtml.ToPlainText();
Console.WriteLine(plainText); // Outputs: Hello World!

string longText = "This is a very long piece of text that needs to be shortened.";
string shortText = longText.Substring(20, "...");
Console.WriteLine(shortText); // Outputs: This is a very long...

string keywords = longText.GetKeywords(5);
Console.WriteLine(keywords); // Example output based on text content.
```

## Contributing
Contributions to the `X.Text` library are welcome. Please ensure to follow the contributing guidelines specified in the repository for submitting issues, feature requests, or pull requests.

## License
The `X.Text` library is released under [MIT license](https://raw.githubusercontent.com/ernado-x/X.Text/master/LICENSE).
