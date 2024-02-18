CREATE DATABASE BooksDb
GO

USE BooksDb
GO

CREATE TABLE Authors (
    AuthorId INT PRIMARY KEY,
    AuthorName NVARCHAR(50) NOT NULL,
    BriefProfile NVARCHAR(150) NOT NULL
);

CREATE TABLE Books (
    BookId INT PRIMARY KEY,
    [Date] DATE NOT NULL,
    BookName NVARCHAR(50) NOT NULL,
    AuthorId INT NOT NULL,
    AuthorName NVARCHAR(50) NOT NULL
    Quantity INT NOT NULL,
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

CREATE TABLE BookAuthors (
    BookId INT,
    AuthorId INT,
    PRIMARY KEY (BookId, AuthorId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId),
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);

-- Insert data into Authors table
INSERT INTO Authors (AuthorId, AuthorName, BriefProfile)
VALUES
    (1, 'GN Shaw', 'Bernard Shaw, was an Irish playwright, critic, polemicist and political activist.'),
    (2, 'Franz Kalka', 'German-speaking Bohemian Jewish novelist and short-story writer based in Prague.'),
    (3, 'Guy the Maupassaul', '19th-century French author, celebrated as a master of the short story');

-- Insert data into Books table
INSERT INTO Books (BookId, BookName, Date, AuthorId, AuthorName, Quantity)
VALUES
    (1, 'Man and Superman', '2023-02-01', 1, 'GN Shaw', 1),
    (2, 'The Castle', '2022-02-01', 2, 'Franz Kalka', 1),
    (3, 'A Woman''s Life', '2024-02-01', 3, 'Guy the Maupassaul', 1);

-- Insert data into BookAuthors table
INSERT INTO BookAuthors (BookId, AuthorId)
VALUES
    (1, 1),
    (2, 2),
    (3, 3);
