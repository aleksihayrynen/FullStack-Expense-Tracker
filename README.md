# 💰 Expense Tracker

A full-stack expense tracking application built with **.NET ASP Core** and **Blazor Pages**.

---

## 🎬 Demo
- Click the image to watch the demo on YouTube.

[![Watch the demo](https://img.youtube.com/vi/xy_DVDMSfPc/0.jpg)](https://www.youtube.com/watch?v=xy_DVDMSfPc)  

---

## 📑 Table of Contents
- [📌- Project Overview](#-project-overview)
- [✨- Key Features](#-key-features)
- [💠- Blazor Architecture](#-blazor-architecture)
- [🛠️- Technologies Used](#%EF%B8%8F-technologies-used)
- [🗄️- Database Design](#%EF%B8%8F-database-design)
- [🚀- Application Functionality](#-application-functionality)
- [🖼️- Screenshots](#%EF%B8%8F-screenshots)

---

## 📌 Project Overview
The **Expense Tracker** allows users to track their income and expenses in a secure and dynamic web application.  
Each user has a personal account with authentication and encrypted passwords. The application leverages **Blazor components** for dynamic rendering and **JavaScript interop** for interactive charts.

---

## ✨ Key Features
- Secure user authentication and identification   
- Dynamic Blazor components for UI rendering  
- Interactive charts using @inject IJSRuntime JS
- Expense and income tracking per user
- Filtering logic for items

---

## 💠 Blazor Architecture
The application makes use of **Blazor Pages**, providing a **component-based architecture**:

- Each page is composed of reusable **Blazor components**    
- JavaScript IJSRuntime JS is used for rendering charts dynamically in combination with Blazor  
- Dynamic components allow smooth UI updates without full page reloads  
- Dependency Injection is used to keep services modular and testable  

This design allows for dynamic updates without the need of JS even though we used it for the charts

---

## 🛠️ Technologies Used
- ASP.NET Core  
- Blazor Pages    
- MongoDB  
- HTML, CSS  

---

## 🗄️ Database Design
MongoDB is used for flexibility and dynamic schema updates.  
Collections include: **Users**, **Expenses**, **Categories**, and **Settings**, allowing easy expansion of features.

---

## 🚀 Application Functionality
Users can:
- Register and log in securely  
- Add, edit, categorize, and delete expenses and income items  
- View interactive charts of expenses  
- Filter by date or category  
- Look at the summary of expense/income  

---

## 🖼️ Screenshots

<table>
  <tr>
    <td>
      <img src="https://github.com/user-attachments/assets/f0989eba-83cc-4075-8f3d-fd787408a3aa" width="100%" />
    </td>
    <td>
      <img src="https://github.com/user-attachments/assets/e2fec4f1-67ac-4b67-a78c-61c38ee1f860" width="100%" />
    </td>
  </tr>
  <tr>
    <td>
      <img src="https://github.com/user-attachments/assets/45d5b937-2d11-49c3-a9d5-841152b5466b" width="100%" />
    </td>
    <td>
      <!-- Optional 4th screenshot -->
    </td>
  </tr>
</table>
