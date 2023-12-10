using LuisDev.TPL;

var stopwatch = AppHelper.StartStopwatch();


var exampleService = new ExampleService();
//await exampleService.AddProducts_Sync_1();
//exampleService.AddProducts_Async_2_NoSafe();
//exampleService.AddProducts_Async_2_InstancingRepository();
//exampleService.AddProducts_Async_2_WithSafeRepository();
//await exampleService.AddProducts_Async_2_SomeProductsNotAdded();
//await exampleService.AddProducts_Async_2_FastAddingAllProducts();
//exampleService.AddProducts_Async_2_InstancingRepository_TakeCare();
//await exampleService.AddProducts_Sync_2_InstancingRepository_TakeCare();


AppHelper.ShowFinalizedData(stopwatch);
