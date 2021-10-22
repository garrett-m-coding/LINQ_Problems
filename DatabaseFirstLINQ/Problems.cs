using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne();
            //ProblemTwo();
            //ProblemThree();
            //ProblemFour();
            //ProblemFive();
            //ProblemSix();
            //ProblemSeven();
            //ProblemEight();
            //ProblemNine();
            //ProblemTen();
            //ProblemEleven();
            //ProblemTwelve();
            //ProblemThirteen();
            //ProblemFourteen();
            //ProblemFifteen();
            //ProblemSixteen();
            //ProblemSeventeen();
            ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count

            Console.WriteLine("********* Problem One **********");
            var users = _context.Users.ToList().Count();
            Console.WriteLine($"There are {users} users.");

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            Console.WriteLine("\n\n\n********* Problem Two **********");
            var users = _context.Users;
            Console.WriteLine("The Users' emails are:");
            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            Console.WriteLine("\n\n\n********* Problem Three **********");
            Console.WriteLine("These are the products greater than $150:");
            var products = _context.Products;
            var over150Price = products.Where(p => p.Price > 150);
            foreach (var product in over150Price)
            {
                Console.WriteLine(product.Name + " " + "$" + product.Price);
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            Console.WriteLine("\n\n\n********* Problem Four **********");
            Console.WriteLine("These are the product names containing an 's':");
            var products = _context.Products;
            var productWithS = products.Where(p => p.Name.Contains("s"));
            foreach (var product in productWithS)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            Console.WriteLine("\n\n\n********* Problem Five **********");
            Console.WriteLine("These are the Users who registered before 2016:");
            var users = _context.Users;
            var dateComparison = new DateTime(2016, 01, 01, 00, 00, 00);
            var before2016Users = users.Where(u => u.RegistrationDate < dateComparison);
            foreach (User user in before2016Users)
            {
                Console.WriteLine(user.Email);
            }
        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            Console.WriteLine("\n\n\n********* Problem Six **********");
            Console.WriteLine("These are the Users who registered after 2016 and before 2018:");
            var users = _context.Users;
            var after2016Date = new DateTime(2016, 01, 01, 00, 00, 00);
            var before2018Date = new DateTime(2018, 01, 01, 00, 00, 00);
            var after2016Before2018 = users.Where(u => u.RegistrationDate > after2016Date && u.RegistrationDate < before2018Date );
           
            foreach (User user in after2016Before2018)
            {
                Console.WriteLine(user.Email + " with registration date of: " + user.RegistrationDate);
            }
        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            Console.WriteLine("\n\n\n********* Problem Seven **********");
            Console.WriteLine("These are the Users who are Customers:");
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            Console.WriteLine("\n\n\n********* Problem Eight **********");
            var afton = _context.Users.Where(u => u.Id == 2);
            Console.WriteLine($"These are User afton@gmail.com's products in shopping cart:");
            var aftonShopCart = _context.ShoppingCarts.Include(u => u.User).Include(u => u.Product).Where(u => u.User.Email == "afton@gmail.com");
            foreach (ShoppingCart product in aftonShopCart)
            {
                Console.WriteLine($"Product Name: {product.Product.Name}     Price: ${product.Product.Price}     Quantity: {product.Quantity}");
            }
        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            Console.WriteLine("\n\n\n********* Problem Nine **********");
            var odaShopCart = _context.ShoppingCarts.Include(u => u.User).Include(u => u.Product).Where(u => u.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine($"Oda's shopping cart total is ${odaShopCart}.");
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            Console.WriteLine("\n\n\n********* Problem Ten **********");
            var employeeUsers = _context.UserRoles
                                        .Include(ur => ur.User)
                                        .Where(ur => ur.RoleId == 2)
                                        .Select(ur => ur.User)
                                        .ToList();

            var empShopCart = _context.ShoppingCarts
                                      .Include(sc => sc.Product)
                                      .Include(sc => sc.User.UserRoles)
                                      .Select(sc => new { sc.User, sc.Product, sc.Quantity, sc.User.UserRoles })
                                      .ToList();

            foreach (var record in empShopCart)
            {
                if (employeeUsers.Contains(record.User))
                {

                    Console.WriteLine($"Email: {record.User.Email }     Product: {record.Product.Name}     $: {record.Product.Price}     Quantity: {record.Quantity}");
                }
            }
        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            Console.WriteLine("\n\n\n********* Problem Eleven **********\n Created new user David");
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            Console.WriteLine("\n\n\n********* Problem Twelve **********\n Created new Product Hat");
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "New Era Brewers 2021",
                Description = "Fitted baseball hat -Royal",
                Price = 35
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
            Console.WriteLine("\n\n\n********* Problem Thirteen **********\n Added 'Customer' role to new User David.");
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            Console.WriteLine("\n\n\n********* Problem Fourteen **********\n Added new product to new User David's shopping cart.");
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "New Era Brewers 2021").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            ShoppingCart newShoppingCart = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId
            };
            _context.ShoppingCarts.Add(newShoppingCart);
            _context.SaveChanges();
        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            Console.WriteLine("\n\n\n********* Problem Fifteen **********\n Updated User David's email to mike@gmail.com.");
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            Console.WriteLine("\n\n\n********* Problem Sixteen **********\n Updated product New Era Brewers Hat's price to its true value of $40.");
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Price == 35).SingleOrDefault();
            product.Price = 40;
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            Console.WriteLine("\n\n\n********* Problem Seventeen **********\n Changed User David's role from customer to employee.");
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            Console.WriteLine("\n\n\n********* Problem Eighteen **********\n Deleted User Oda's customer role and replaced with 'null'.");
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in
            // a. Give them a menu where they perform the following actions within the console
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in
            // a. Display "Invalid Email or Password"
            // b. Re-prompt the user for credentials

        }

    }
}
