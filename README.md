# AzureHelper - A console application that helps to intract with Azure data factory and Azure Search
This is a sample project that helps to 
- Run a pipeline in Azure dataFactoy
- Do a search using Azure Cognitive search(now Azure AI Search)

# Technical specfications  
- Framework:.net framework 4.8
- IDE: Visual Studio 2022
- Language: C#
- UI: Console Application/Command-Line pplication

# Azure Settings for datafactory
- Create datasource(Sql DB,blob file etc), target(sql DB etc), other required resources.
- Create a datafactory pipeline in Azure
- Reference:
  - https://learn.microsoft.com/en-us/azure/data-factory/tutorial-copy-data-portal
  - https://www.youtube.com/watch?v=EpDkxTHAhOs
- Follow the latest tutorials mentioned in Azure tutorials.
  
# Azure Settings for search
- Create a Azure AI Search resource
- Refer https://learn.microsoft.com/en-us/azure/search/search-create-service-portal
- Create an index (this index will be refered from console application)
- Follow the latest tutorials mentioned in Azure tutorials.
  
# Usage
- Open the solution file in Visual Studio
- Go to EIdConsoleApp/Program.cs and set your Azure details.
- Build the code and launch the application
- If you want to pass a search string pass it as command line argument. 

# Log
- Log.txt will be created in application folder incase of error.

