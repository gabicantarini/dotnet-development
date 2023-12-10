using LuisDev.TPL.Infra;
using System.Reflection.Metadata.Ecma335;

namespace LuisDev.TPL
{
    internal class ExampleService
    {
        public async Task AddProducts_Sync_1()
        {
            var productRepository = new ProductRepository();
            
            var products = GenerateProducts();
            foreach (var product in products)
            {
                if (!product.IsValid()) continue;

                await productRepository.AddProduct(product);
            }
        }
        public void AddProducts_Async_2_NoSafe()
        {
            var productRepository = new ProductRepository();

            var products = GenerateProducts();
            Parallel.ForEach(products, product =>
            {
                if (!product.IsValid()) return;

                productRepository.AddProduct(product).Wait();
            });
        }
        public void AddProducts_Async_2_InstancingRepository()
        {
            var products = GenerateProducts();

            Parallel.ForEach(products, (product) =>
            {
                var productRepository = new ProductRepository();

                if (!product.IsValid()) return;
                productRepository.AddProduct(product).Wait();
            });
        }
        public void AddProducts_Async_2_WithSafeRepository()
        {
            var productRepository = new ProductSafeRepository();
            var products = GenerateProducts();

            Parallel.ForEach(products, (product) =>
            {
                if (!product.IsValid()) return;
                productRepository.AddProduct(product).Wait();
            });

        }
        public async Task AddProducts_Async_2_SomeProductsNotAdded()
        {
            var productRepository = new ProductRepository();

            var products = GenerateProducts();
            var validProducts = new List<Product>();
            var tasksValidateProducts = new List<Task>();

            foreach (var product in products)
            {
                var task = Task.Run(() =>
                {
                    if (product.IsValid())
                    {
                        validProducts.Add(product);
                    }
                });
                tasksValidateProducts.Add(task);
            }
            await Task.WhenAll(tasksValidateProducts);

            foreach (var product in validProducts)
            {
                await productRepository.AddProduct(product);
            }
        }       
        public async Task AddProducts_Async_2_FastAddingAllProducts()
        {
            var productRepository = new ProductRepository();

            var products = GenerateProducts();
            var lockObject = new object();
            var validProducts = new List<Product>();
            var tasksValidateProducts = new List<Task>();

            foreach (var product in products)
            {
                var task = Task.Run(() =>
                {
                    if (product.IsValid())
                    {
                        lock (lockObject)
                        {
                            validProducts.Add(product);
                        }
                    }
                });
                tasksValidateProducts.Add(task);
            }
            await Task.WhenAll(tasksValidateProducts);

            foreach (var product in validProducts)
            {
                await productRepository.AddProduct(product);
            }
        }
        public void AddProducts_Async_2_InstancingRepository_TakeCare()
        {
            var products = GenerateProducts().Take(3);

            Parallel.ForEach(products, (product) =>
            {
                var productRepository = new ProductRepository();

                if (!product.IsValid()) return;
                productRepository.AddProduct(product).Wait();
            });

        }
        public async Task AddProducts_Sync_2_InstancingRepository_TakeCare()
        {
            var productRepository = new ProductRepository();

            var products = GenerateProducts().Take(3);
            foreach (var product in products)
            {
                if (!product.IsValid()) continue;

                await productRepository.AddProduct(product);
            }

        }
        static List<Product> GenerateProducts()
        {
            return new List<Product>(60)
            {
                new Product() { Id = Guid.NewGuid(), Name = "Bike", Price = 1000 },
                new Product() { Id = Guid.NewGuid(), Name = "Car", Price = 100000 },
                new Product() { Id = Guid.NewGuid(), Name = "Boat", Price = 200000 },
                new Product() { Id = Guid.NewGuid(), Name = "Jet", Price = 1000000 },
                new Product() { Id = Guid.NewGuid(), Name = "Computer", Price = 2000 },
                new Product() { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
                new Product() { Id = Guid.NewGuid(), Name = "Keyboard", Price = 50 },
                new Product() { Id = Guid.NewGuid(), Name = "Monitor", Price = 500 },
                new Product() { Id = Guid.NewGuid(), Name = "Pen", Price = 2 },
                new Product() { Id = Guid.NewGuid(), Name = "Pencil", Price = 1 },
                new Product() { Id = Guid.NewGuid(), Name = "Bike", Price = 1000 },
                new Product() { Id = Guid.NewGuid(), Name = "Car", Price = 100000 },
                new Product() { Id = Guid.NewGuid(), Name = "Boat", Price = 200000 },
                new Product() { Id = Guid.NewGuid(), Name = "Jet", Price = 1000000 },
                new Product() { Id = Guid.NewGuid(), Name = "Computer", Price = 2000 },
                new Product() { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
                new Product() { Id = Guid.NewGuid(), Name = "Keyboard", Price = 50 },
                new Product() { Id = Guid.NewGuid(), Name = "Monitor", Price = 500 },
                new Product() { Id = Guid.NewGuid(), Name = "Pen", Price = 2 },
                new Product() { Id = Guid.NewGuid(), Name = "Pencil", Price = 1 },
                new Product() { Id = Guid.NewGuid(), Name = "Bike", Price = 1000 },
                new Product() { Id = Guid.NewGuid(), Name = "Car", Price = 100000 },
                new Product() { Id = Guid.NewGuid(), Name = "Boat", Price = 200000 },
                new Product() { Id = Guid.NewGuid(), Name = "Jet", Price = 1000000 },
                new Product() { Id = Guid.NewGuid(), Name = "Computer", Price = 2000 },
                new Product() { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
                new Product() { Id = Guid.NewGuid(), Name = "Keyboard", Price = 50 },
                new Product() { Id = Guid.NewGuid(), Name = "Monitor", Price = 500 },
                new Product() { Id = Guid.NewGuid(), Name = "Pen", Price = 2 },
                new Product() { Id = Guid.NewGuid(), Name = "Pencil", Price = 1 },
                new Product() { Id = Guid.NewGuid(), Name = "Bike", Price = 1000 },
                new Product() { Id = Guid.NewGuid(), Name = "Car", Price = 100000 },
                new Product() { Id = Guid.NewGuid(), Name = "Boat", Price = 200000 },
                new Product() { Id = Guid.NewGuid(), Name = "Jet", Price = 1000000 },
                new Product() { Id = Guid.NewGuid(), Name = "Computer", Price = 2000 },
                new Product() { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
                new Product() { Id = Guid.NewGuid(), Name = "Keyboard", Price = 50 },
                new Product() { Id = Guid.NewGuid(), Name = "Monitor", Price = 500 },
                new Product() { Id = Guid.NewGuid(), Name = "Pen", Price = 2 },
                new Product() { Id = Guid.NewGuid(), Name = "Pencil", Price = 1 },
                new Product() { Id = Guid.NewGuid(), Name = "Bike", Price = 1000 },
                new Product() { Id = Guid.NewGuid(), Name = "Car", Price = 100000 },
                new Product() { Id = Guid.NewGuid(), Name = "Boat", Price = 200000 },
                new Product() { Id = Guid.NewGuid(), Name = "Jet", Price = 1000000 },
                new Product() { Id = Guid.NewGuid(), Name = "Computer", Price = 2000 },
                new Product() { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
                new Product() { Id = Guid.NewGuid(), Name = "Keyboard", Price = 50 },
                new Product() { Id = Guid.NewGuid(), Name = "Monitor", Price = 500 },
                new Product() { Id = Guid.NewGuid(), Name = "Pen", Price = 2 },
                new Product() { Id = Guid.NewGuid(), Name = "Pencil", Price = 1 },
                new Product() { Id = Guid.NewGuid(), Name = "Bike", Price = 1000 },
                new Product() { Id = Guid.NewGuid(), Name = "Car", Price = 100000 },
                new Product() { Id = Guid.NewGuid(), Name = "Boat", Price = 200000 },
                new Product() { Id = Guid.NewGuid(), Name = "Jet", Price = 1000000 },
                new Product() { Id = Guid.NewGuid(), Name = "Computer", Price = 2000 },
                new Product() { Id = Guid.NewGuid(), Name = "Mouse", Price = 20 },
                new Product() { Id = Guid.NewGuid(), Name = "Keyboard", Price = 50 },
                new Product() { Id = Guid.NewGuid(), Name = "Monitor", Price = 500 },
                new Product() { Id = Guid.NewGuid(), Name = "Pen", Price = 2 },
                new Product() { Id = Guid.NewGuid(), Name = "Pencil", Price = 1 },
            };
        }
    }
}
