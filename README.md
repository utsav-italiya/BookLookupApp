# BookLookupApp

BookLookupApp is a .NET 8 application using Razor Pages, designed to look up book details via an ISBN number through the BookConnect API. It provides a simple, one-page interface to retrieve and display book information including title, subtitle, cover image, price, description, authors, and subject.

## Features

- **ISBN Lookup**: Enter an ISBN (International Standard Book Number) to retrieve book details.
- **Book Details**: Displays book title, subtitle, cover image, price, description, authors, and subject.
- **API Integration**: Fetches book data from the "BookConnect" API.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Postman](https://www.postman.com/) (for testing API calls)


## Installation & Setups

### Clone the Repository and Navigate into the project directory

- git clone https://github.com/utsav-italiya/BookLookupApp.git

- Open CMD (Command Prompt) Terminal

- Fire below command
```
$ cd BookLookupApp
```
### Install Dependencies
- Use below command to install all the dependencies used in the application
```
$ dotnet restore
```
### Configuration

- Create an "appsettings.json" file with the following content:
- SAMPLE BCI KEY: "jHuB7IY1mZfLiS3VLXuvFF3PaB3TigZ1TXypOADo0jo4sasgJt6b2heDFAGev62J"
```
{
  "Logging": {
      "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
      }
  },
  "AllowedHosts": "*",
  "BookConnectApiUrl": "https://app.bookconnect.ca/api",
  "BookConnectApiKey": "<YOUR_BCI_KEY>"
}
```

### Build and Run the Application
```
$ dotnet build
```
```
$ dotnet run
```
### HOT Reload

- To enable hot reload for the application

```
$ dotnet watch
```


## Usage

1. Open your web browser and navigate to http://localhost:5146.
2. Enter an ISBN number in the input field on the main page.
3. Click the "Search" button to retrieve and display the book details.

## Application Workflow

### Main Screen

The main screen provides an input field for entering the ISBN and a "Search" button. Upon submission, the application calls the `ITitleApiClient` to fetch and display book information.

### Display

The book details are displayed in a professional format, showing:

- **Title:** The title of the book.
- **Subtitle:** The subtitle of the book, if available.
- **Cover Image:** The URL of the book's cover image.
- **Price:** The price of the book in USD.
- **Description:** A description of the book.
- **Authors:** A list of authors of the book.
- **Subject:** The subject of the book.

### API Integration

#### API Endpoint

The API endpoint for fetching book information is:

 - https://app.bookconnect.ca/api/titles/json/{isbn}
 
To test the API use API testing tool *recommended* **Postman.**

- Request type *GET*
- Add Header bci-key, for value use API key that is mention below
- Click on Send button

#### API Key

Use the following API key for accessing test data:

  - jHuB7IY1mZfLiS3VLXuvFF3PaB3TigZ1TXypOADo0jo4sasgJt6b2heDFAGev62J

#### Sample ISBNs for Testing

- 9780764363061
- 9780764363245
- 9780764363252
- 9780764363269

### Service Layer

#### TitleApiClient

The `TitleApiClient` class implements the `ITitleApiClient` interface and is responsible for making HTTP requests to the BookConnect API and parsing the JSON response into a `TitleDto`.

#### ITitleApiClient

The `ITitleApiClient` interface defines the method:

```
Task<TitleDto> GetTitleAsync(string isbn);
```
#### TitleDto Structure

The `TitleDto` class is used to represent the book details.

## Contact

For questions or feedback, please contact Utsav Italiya (utsavitaliya085@gmail.com).