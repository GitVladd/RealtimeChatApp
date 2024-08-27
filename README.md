# Realtime Chat App

This project is a real-time chat application built using ASP.NET Core, SignalR, and Azure services. The app allows users to chat in real-time, with integrated sentiment analysis using Azure Cognitive Services.

## Features

- **Real-time Communication**: Users can chat in real-time.
- **Sentiment Analysis**: Messages are analyzed for sentiment, and results are displayed in real-time.
- **Azure Integration**: Chat data is stored in Azure SQL Database, and the app is deployed to Azure.

## Technologies Used

- ASP.NET Core
- Azure SignalR Service
- Azure Cognitive Services (Text Analytics)
- Azure SQL Database

## Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/GitVladd/RealtimeChatApp.git
   ```
2. **Configure Azure Services**:
   - Set up Azure SignalR Service, SQL Database, and Cognitive Services.
   - Store your keys and connection strings securely (e.g., using Azure Key Vault).

3. **Add Required Connection Strings**:
   - **Azure SignalR Service**: `AzureSignalRService:ConnectionString`
   - **Azure SQL Database**: `AzureSQLDatabase:ConnectionString`
   - **Azure Cognitive Services (Text Analytics)**: `AzureSentimentAnalysisService:Key` and `AzureSentimentAnalysisService:Endpoint`

4. **Build and Run**:
   - Navigate to the project directory.
   - Restore dependencies: `dotnet restore`
   - Build the project: `dotnet build`
   - Run the project: `dotnet run`

5. **Apply migrations**:
   - `dotnet ef database update`

6. **Deploy to Azure**:
   - Follow the Azure deployment guidelines to publish the app.

