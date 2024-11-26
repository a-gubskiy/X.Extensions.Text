# X.Extensions.Text

[![NuGet Version](http://img.shields.io/nuget/v/X.Extensions.Text.svg?style=flat)](https://www.nuget.org/packages/X.Extensions.Text/)
[![Twitter URL](https://img.shields.io/twitter/url/https/twitter.com/andrew_gubskiy.svg?style=social&label=Follow%20me!)](https://twitter.com/intent/user?screen_name=andrew_gubskiy)


The `TextHelper` class provides utility methods for common text manipulation tasks such as cleaning, substringing, replacing characters, and extracting keywords. 

This library is designed to simplify text processing in .NET applications.

## Installation

Include the `X.Extensions.Text` namespace in your project to use the `TextHelper` class.

```csharp
using X.Extensions.Text;
```

## Methods

### Substring(string text, int length)

Retrieves a substring starting from the beginning of the given text, limited to the specified length.

#### Example
```csharp
string text = "This is a long text.";
string result = TextHelper.Substring(text, 10);
Console.WriteLine(result); // Output: "This is a "
```

---

### Substring(string text, int length, string endPart)

Retrieves a substring with the option to append a custom ending (e.g., "...") if the text is truncated.

#### Example
```csharp
string text = "This is a long text.";
string result = TextHelper.Substring(text, 10, "...");
Console.WriteLine(result); // Output: "This is..."
```

---

### CleanCharacters(string text)

Cleans system characters and replaces them with hyphens. Ensures no double spaces and trims the result.

#### Example
```csharp
string text = "Hello, world! This is a test.";
string result = TextHelper.CleanCharacters(text);
Console.WriteLine(result); // Output: "hello-world-this-is-a-test"
```

---

### Replace(string text, IEnumerable<string> targets, string replacement)

Replaces all occurrences of the specified target strings in the input text with the replacement string.

#### Example
```csharp
string text = "Hello, world!";
string result = TextHelper.Replace(text, new[] { "Hello", "world" }, "Hi");
Console.WriteLine(result); // Output: "Hi, Hi!"
```

---

### ToPlainText(string text)

Converts HTML to plain text by removing tags and special symbols.

#### Example
```csharp
string html = "<p>This is <b>HTML</b>.</p>";
string result = TextHelper.ToPlainText(html);
Console.WriteLine(result); // Output: "This is HTML."
```

---

### ToPlainText(string text, bool preserveLineBreaks)

Converts HTML to plain text, preserving line breaks if specified.

#### Example
```csharp
string html = "<p>This is a line.<br/>Another line.</p>";
string result = TextHelper.ToPlainText(html, true);
Console.WriteLine(result); // Output: "This is a line.<br />Another line."
```

---

### GetKeywords(string text, int count)

Extracts the top unique keywords from the text, ordered by frequency.

#### Example
```csharp
string text = "This is a sample text. Sample text is important.";
string keywords = TextHelper.GetKeywords(text, 3);
Console.WriteLine(keywords); // Output: "sample, text, important"
```

---

### CutText(string text, int maxLength = 200)

Cuts the text to the specified length, attempting to preserve logical blocks by stopping at the nearest sentence-ending dot.

#### Example
```csharp
string text = "This is a long text. It has multiple sentences.";
string result = TextHelper.CutText(text, 20);
Console.WriteLine(result); // Output: "This is a long text."
```

---

### TrimLineBreaksFromStart(string text, string lineBreakPlaceholder)

Removes line break placeholders from the beginning of the text.

#### Example
```csharp
string text = "[[LINE_BREAK]][[LINE_BREAK]]Hello!";
string result = TextHelper.TrimLineBreaksFromStart(text);
Console.WriteLine(result); // Output: "Hello!"
```

---

## Contributing
Contributions to the `X.Extensions.Text` library are welcome. Please ensure to follow the contributing guidelines specified in the repository for submitting issues, feature requests, or pull requests.

## License
The `X.Extensions.Text` library is released under [MIT license](https://raw.githubusercontent.com/ernado-x/X.Text/master/LICENSE).
