A base test framework structure which is built on top of the saucedemo web site (https://www.saucedemo.com/)

The framework supports parameterization, to use it use the following command:

# Supported options for browser: 1 - Chrome, 2 - Firefox, 3 - Edge
dotnet test saucedemotests.csproj -- TestRunParameters.Parameter\(name=\"browserID\",value=\"1\"\) TestRunParameters.Parameter\(name=\"baseURL\",value=\"https://www.saucedemo.com/\"\)
