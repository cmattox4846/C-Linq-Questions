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
            // ProblemFour();
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
            //ProblemEighteen();
            //ProblemNineteen();
            //ProblemTwenty();
            //BonusOne();
            //BonusTwo();
            BonusThree();


        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private int ProblemOne()
        {

            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count
            return _context.Users.ToList().Count;

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.

            var products = _context.Products;
            var products150AndGreater = products.Where(p => p.Price > 150).ToList();
            foreach (Product product in products150AndGreater)
            {
                Console.WriteLine(product.Name, product.Price);
            }
            


        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            var productsThatContainS = products.Where(p => p.Name.Contains("s"));
            foreach (Product product in productsThatContainS)
            {
                Console.WriteLine(product.Name);
            }  
        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            DateTime endDate = new DateTime(2016, 01, 01);
            var users = _context.Users;
            var registeredBefore2016 = users.Where(u => u.RegistrationDate < endDate).ToList();
            foreach (User user in registeredBefore2016)
            {
                //var date = user.RegistrationDate.ToString();
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
                //Console.WriteLine(user.RegistrationDate);

            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            DateTime startDate = new DateTime(2017, 01, 01);
            DateTime endDate = new DateTime(2018, 01, 01);
            var users = _context.Users;
            var registeredAfter2016Before2018 = users.Where(u => u.RegistrationDate >= startDate && u.RegistrationDate < endDate  ).ToList();
            foreach (User user in registeredAfter2016Before2018)
            {
                Console.WriteLine(user.Email + " " + user.RegistrationDate);
            }
            

        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
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
            var productsInCart = _context.ShoppingCarts.Include(p => p.User).Include(p => p.Product).Where(p => p.User.Email == "oda@gmail.com");
            
                
            foreach (ShoppingCart product in productsInCart)
            {
                Console.WriteLine($"Product Name: {product.Product.Name} Product Price: {product.Product.Price} Quantity:{product.Quantity}");
            }
                
         }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.

            var productsInCart = _context.ShoppingCarts.Include(p => p.User).Include(p => p.Product).Where(p => p.User.Id == 2).Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(productsInCart);


        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.


            var userIds = _context.UserRoles.Include(u => u.Role).Include(u => u.User).Where(r => r.Role.RoleName == "Employee").Select(s => s.User.Id);
            var productsInCart = _context.ShoppingCarts.Include(p => p.User).Include(p => p.Product).Where(p => userIds.Contains(p.UserId));

            foreach (ShoppingCart scRow in productsInCart)
            {
                Console.WriteLine($"User's Email: {scRow.User.Email} Product Name: {scRow.Product.Name} Product Price: {scRow.Product.Price} Quantity:{scRow.Quantity}");
            }

        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
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
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Ibenez Gio Electric Guitar",
                Description = "Great guitar for beginners learning how to play electric guitar for the first time!",
                Price = 150

            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();
        }

        private void ProblemThirteen()
        {
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
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productID = _context.Products.Where(p => p.Name == "Ibenez Gio Electric Guitar").Select(p =>p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "oda@gmail.com").Select(u => u.Id).SingleOrDefault();

            ShoppingCart newProduct = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productID,
                Quantity = 1
            };
            _context.ShoppingCarts.Add(newProduct);
            _context.SaveChanges();
        }



    

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var product = _context.Products.Where(p => p.Name == "Ibenez Gio Electric Guitar").SingleOrDefault();
            product.Price = 100;
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
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
            var userRecord = _context.Users.Where(u => u.Email == "oda@gmail.com");
            foreach (User list in userRecord)
            {
                _context.Users.Remove(list);
            }
            _context.SaveChanges();
        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Please enter email address.");
            string userEmail = Console.ReadLine();
            Console.WriteLine("Please enter password.");
            string userPassword = Console.ReadLine();

            
            var userExists = _context.Users.Where(u => u.Email.Contains(userEmail) && u.Password.Contains(userPassword)).SingleOrDefault();
           
            if(userExists == null)
            {
                Console.WriteLine("Invaild Email  or password."); 
            }
            else if (userExists != null)
            {
                Console.WriteLine("Signed In!");
            }

        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
           
            var usersList = _context.Users.Select(u => u.Id).ToList();
            decimal grandtotal = 0;
            foreach (var user in usersList)
            {
                var shoppingCartItems = _context.ShoppingCarts.Include(sc => sc.User).Where(sc => sc.UserId == user).Select(sc => sc.Product.Price).Sum();
               
                // Display the total of each users shopping cart as well as the total of the toals to the console.
                Console.WriteLine($"Total of products: {shoppingCartItems}  User Id: {user}");
                grandtotal += shoppingCartItems;
                Console.WriteLine($"Running Total: {grandtotal}");

            }



        }

        // BIG ONE
        private void BonusThree()

        {
            SignIn();
            // 1. Create functionality for a user to sign in via the console
            void SignIn()
            {
                Console.WriteLine("Please enter email address.");
                string userEmail = Console.ReadLine();
                Console.WriteLine("Please enter password.");
                string userPassword = Console.ReadLine();


                var userExists = _context.Users.Where(u => u.Email.Contains(userEmail) && u.Password.Contains(userPassword)).SingleOrDefault();

                if (userExists == null)
                {
                    // 3. If the user does not succesfully sign in
                   
                    // a. Display "Invalid Email or Password"
                    
                    Console.WriteLine("Invaild Email  or password.");
                   
                    // b. Re-prompt the user for credentials
                    SignIn();

                }
                else if (userExists != null)
                {
                    // 2. If the user succesfully signs in
                    Console.WriteLine("Signed In!");
                    MenuOptions(userEmail);
                }

            }

            // a. Give them a menu where they perform the following actions within the console
             void MenuOptions(string userData)
            {
                Console.WriteLine("Please choose an option 1 - View Shopping Cart 2 - View All Products 3 - Add Product To Shopping Cart 4 - Remove Product From Shopping Cart 5 - To Quit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    // View the products in their shopping cart
                    case 1: 
                        var productsInCart = _context.ShoppingCarts.Include(p => p.User).Include(p => p.Product).Where(p => p.User.Email == userData);


                        foreach (ShoppingCart product in productsInCart)
                        {
                        Console.WriteLine($"Product Name: {product.Product.Name} Product Price: {product.Product.Price} Quantity:{product.Quantity}");
                        }
                        MenuOptions(userData);
                        break;
                    // View all products in the Products table
                    case 2:
                        var allProducts = _context.Products;
                        foreach (var product in allProducts)
                        {
                            Console.WriteLine($"Product Name: {product.Name} Product Description: {product.Description} Product Price: {product.Price}");
                        }
                        MenuOptions(userData);
                        break;
                    // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
                    case 3:
                        Console.WriteLine("Name of Product to be Added");
                        var productToBeAdded = Console.ReadLine();
                        var productAlreadyInCart = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => sc.User.Email == userData &&  sc.Product.Name == productToBeAdded).SingleOrDefault();
                        if (productAlreadyInCart != null)
                        {
                            var product = _context.ShoppingCarts.Where(sc => sc.Product.Name == productToBeAdded).FirstOrDefault();
                            product.Quantity++;
                            _context.SaveChanges();
                        } else if (productAlreadyInCart == null)
                        { 
                            var productInfo = _context.Products.Where(p => p.Name == productToBeAdded).SingleOrDefault();
                            ShoppingCart newCartEntry = new ShoppingCart()
                            {
                                ProductId = productInfo.Id,
                                UserId = _context.Users.Where(u => u.Email == userData).Select(u => u.Id).FirstOrDefault()
                            };
                            _context.ShoppingCarts.Add(newCartEntry);
                            _context.SaveChanges();
                        }
                        MenuOptions(userData);
                        break;
                    // Remove a product from their shopping cart
                    case 4:
                        Console.WriteLine("Name of Product to be Removed");
                        var productToBeRemoved = Console.ReadLine();
                        var itemToRemove = _context.ShoppingCarts.Where(sc => sc.Product.Name == productToBeRemoved).SingleOrDefault();

                        _context.ShoppingCarts.Remove(itemToRemove);
                        _context.SaveChanges();


                        MenuOptions(userData); 
                        break;
                    case 5:
                        Console.WriteLine("Bye!!");

                        
                        break;

                    //No valid option repromt for input
                    default: Console.WriteLine("Please choose valid option!");
                        MenuOptions(userData);
                        break;
                
                }
            }

        }


        
        
       
       
       
       
       

    

    }
}
