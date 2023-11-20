Overview

This repository contains the implementation of a web application designed to manage two domains: Products and Categories. The application provides a user interface (UI) built with the Angular framework for managing products and categories, and an API developed in C# on the .NET platform to handle CRUD operations and interact with a NoSQL database.
Domains
Product

    Id: Unique identifier for the product.
    Name: Product name with a maximum length of 100 characters (required).
    Description: Product description with a maximum length of 150 characters, required and should contain the name of the product inside.
    Price: Product price (required).
    Category: Category to which the product belongs (required).
    Color: Color of the product.

Category

    Id: Unique identifier for the category.
    Name: Category name (required).
    Description: Category description (required).

Functionality

The application implements CRUD (Create, Read, Update, Delete) functionality for both products and categories. It ensures proper validation based on the specified details.
Technologies Used

    UI: Angular framework
    API: C# language on the .NET platform
    Database: NoSQL (from the developer's preference)

Code Quality

The implementation follows coding principles, readability, best practices, and a sound design architecture. Unit tests have been implemented to cover various scenarios, ensuring robust functionality.
Database Design

The database design aligns with the application's requisites, ensuring efficient storage and retrieval of product and category information.
