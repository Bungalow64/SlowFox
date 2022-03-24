# Rules for SlowFox.UnitTestMocks.MSTest

## SFMK001 Cannot Generate Mocks

This is a warning that is shown if the mocks for a specific class cannot be generated, because of an unexpected error.  The details of the error will be included in the description of the warning.

If you find this warning appearing in your project, please raise this in the [Issues page in GitHub](https://github.com/Bungalow64/SlowFox/issues).

## SFMK002 Only Classes Allowed As Target

This is a warning because the type referenced in the InjectMocksAttribute is not a class, which isn't allowed since a class is required to determine which mocks to generate.  Update the attribute to reference a class instead.

## SFMK003 Missing Dependency

A required dependency has not been found.  The warning will indicate which package is required.  Please install it from NuGet.

## SFMK004 Invalid Config Option

An unexpected value has been found for a configuration option.  Check the documentation for the expected values.

