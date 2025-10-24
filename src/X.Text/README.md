# X.Text (DEPRECATED)

[![Sponsor on GitHub](https://img.shields.io/badge/Sponsor_on_GitHub-ff7f00?logo=github&logoColor=white&style=for-the-badge)](https://github.com/sponsors/a-gubskiy)
[![Subscribe on X](https://img.shields.io/badge/Subscribe_on_X-000000?logo=x&logoColor=white&style=for-the-badge)](https://x.com/andrew_gubskiy)
[![NuGet Downloads](https://img.shields.io/nuget/dt/X.Extensions.Text?style=for-the-badge&label=NuGet%20Downloads&color=004880&logo=nuget&logoColor=white)](https://www.nuget.org/packages/X.Extensions.Text)

This project/package has been migrated into `X.Extensions.Text` and is no longer maintained here.

Please switch to the `X.Extensions.Text` package which contains improvements and ongoing maintenance.

Quick migration steps:

1. Remove the old package (if installed):

```bash
dotnet remove package X.Text
```

2. Add the new package:

```bash
dotnet add package X.Extensions.Text
```

3. Update your using statements (if present):

```csharp
// Before
using X.Text;

// After
using X.Extensions.Text;
```

If you depend on a specific API and can't find it in `X.Extensions.Text`, please open an issue in the repository, and we'll help with the mapping or provide a compatibility suggestion.
