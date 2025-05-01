# EcommerceApp
# 🛒 E-Commerce Platform (MAUI)

This is a multi-cart e-commerce app built using **.NET MAUI** that supports inventory management, 
a customizable tax rate, multiple shopping carts (including a wishlist), and dynamic real-time updates to product quantities.
---
## 🎯 Features

✅ **Inventory Management**  
- Add, update, or delete products  
- Inline text box with "+" button to quickly add a specific quantity to the cart  
- Inventory adjusts in real time when items are added to or removed from any cart  

✅ **Shopping Cart(s)**  
- View selected cart and its contents  
- Increase/decrease product quantity inline  
- Automatically calculates subtotal, tax, and total  
- Scrollable view for large carts

✅ **Wishlist Support**  
- Users can switch between multiple carts using a cart picker  
- Add products to a selected cart from inventory  
- All carts persist in memory during app session  

✅ **Tax Settings**  
- Configure tax rate via a dedicated settings screen  
- Saved value is used for real-time calculations in cart and checkout

✅ **Sorting**  
- Sort inventory and cart by **name** or **price**

---

## 🧑‍💻 How to Run the Project

> This project uses **.NET MAUI** and is best opened using **Visual Studio 2022 or later (Community Edition)**.

### 📦 Prerequisites:
- Visual Studio Community 2022 (or later)
- .NET MAUI workload installed

### ▶️ Steps to Run:

1. **Clone or download** the repository
2. Open the solution (`.sln`) in **Visual Studio**
3. Select the target platform (e.g. Windows Machine or Android Emulator)
4. Set `EcommerceApp` (or the correct project name) as the **Startup Project**
5. Click **Start** (▶) or press `F5` to build and run

---

## 🎥 Demo Video
📺 [Watch the YouTube Demo](https://youtu.be/4Ca4nIJjWXA)

---
 
## 📁 Folder Structure

EcommerceApp/
├── App.xaml
├── App.xaml.cs
├── AppShell.xaml
├── AppShell.xaml.cs
├── MainPage.xaml
├── MainPage.xaml.cs
├── Models/
│   ├── Product.cs
│   └── CartItem.cs
├── Services/
│   ├── ProductService.cs
│   └── ShoppingCartService.cs
├── Views/
│   ├── InventoryPage.xaml
│   ├── InventoryPage.xaml.cs
│   ├── ShoppingCartPage.xaml
│   ├── ShoppingCartPage.xaml.cs
│   ├── CheckoutPage.xaml
│   ├── CheckoutPage.xaml.cs
│   ├── TaxConfigPage.xaml
│   ├── TaxConfigPage.xaml.cs
│   └── MainMenuPage.xaml
│       MainMenuPage.xaml.cs
├── Resources/
│   ├── Fonts/
│   ├── Images/
│   │   └── dotnet_bot.png
│   └── Styles/
│       └── Styles.xaml
├── Platforms/
│   ├── Android/
│   ├── iOS/
│   ├── MacCatalyst/
│   └── Windows/
├── wwwroot/
├── bin/
├── obj/
└── README.md

