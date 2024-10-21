using VseTShirts.Areas.Admin.Models;
using VseTShirts.DB.Models;
using VseTShirts.Models;

namespace VseTShirts;

public static class Helper
{
    public static int RandomNumber(this int n)
    {
        Random random = new Random();
        return random.Next(n);
    } 
    public static List<ProductViewModel> ToViewModel(this List<Product> list)
    {
        var products = new List<ProductViewModel>();
        foreach (var product in list)
        {
            products.Add(ToViewModel(product));
        }
        return products;
    }
    public static ProductViewModel ToViewModel(this Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Quantity = product.Quantity,
            Sex = product.Sex,
            ImagePath = product.ImagePath,
            Category = product.Category,
            Color = product.Color,
            Size = product.Size,
        };
    }


    public static List<Product> ToDBModel(this List<ProductViewModel> list)
    {
        var products = new List<Product>();
        foreach (var product in list)
        {
            products.Add(ToDBModel(product));
        }
        return products;
    }
    public static Product ToDBModel(this ProductViewModel productVM)
    {
        return new Product
        {
            Id = productVM.Id,
            Name = productVM.Name,
            Description = productVM.Description,
            Price = productVM.Price,
            Quantity = productVM.Quantity,
            Sex = productVM.Sex,
            ImagePath = productVM.ImagePath,
            Category = productVM.Category,
            Color = productVM.Color,
            Size = productVM.Size,
        };
    }

    public static OrderViewModel ToViewModel(this Order order)
    {
        return new OrderViewModel
        {
            Id = order.Id,
            Address = order.Address,
            Name = order.Name,
            Status = ToViewModel(order.Status),
            Cart = new CartViewModel { UserId = order.UserId, Positions = order.Positions.Select(p => ToViewModel(p)).ToList()},
            Phone = order.Phone,
            DateAndTime = order.DateAndTime,
            UserId = order.UserId
        };
    }
    public static List<OrderViewModel> ToViewModel(this List<Order> orders)
    {
        return orders.Select(o => ToViewModel(o)).ToList();
    }
    public static Order ToDBModel(this OrderViewModel orderViewMOdel)
    {
        return new Order
        {
            Id = orderViewMOdel.Id,
            Address = orderViewMOdel.Address,
            Name = orderViewMOdel.Name,
            Status = ToDBModel(orderViewMOdel.Status),
            Positions = ToDBModel(orderViewMOdel.Cart.Positions),
            Phone = orderViewMOdel.Phone,
            DateAndTime = orderViewMOdel.DateAndTime,
            UserId = orderViewMOdel.UserId
        };
    }


    public static OrderStatusViewModel ToViewModel(this OrderStatus orderStatus)
    {
        switch (orderStatus)
        {
            case OrderStatus.Создан: return OrderStatusViewModel.Создан;
            case OrderStatus.Одобрен: return OrderStatusViewModel.Одобрен;
            case OrderStatus.В_Доставке: return OrderStatusViewModel.В_Доставке;
            case OrderStatus.Ожидает_Получения: return OrderStatusViewModel.Ожидает_Получения;
            case OrderStatus.Получен: return OrderStatusViewModel.Получен;
            default: return OrderStatusViewModel.Error;
        }
    }
    public static OrderStatus ToDBModel(this OrderStatusViewModel orderStatus)
    {
        switch (orderStatus)
        {
            case OrderStatusViewModel.Создан: return OrderStatus.Создан;
            case OrderStatusViewModel.Одобрен: return OrderStatus.Одобрен;
            case OrderStatusViewModel.В_Доставке: return OrderStatus.В_Доставке;
            case OrderStatusViewModel.Ожидает_Получения: return OrderStatus.Ожидает_Получения;
            case OrderStatusViewModel.Получен: return OrderStatus.Получен;
            default: return OrderStatus.Error;
        }
    }
    private static List<CartPosition> ToDBModel(this List<CartPositionViewModel> items)
    {
        List<CartPosition> cartPositions = new List<CartPosition>();
        foreach (var item in items)
        {
            cartPositions.Add(ToDBModel(item));
        }
        return cartPositions;
    }
    public static CartPositionViewModel ToViewModel(this CartPosition cartPosition)
    {
        return new CartPositionViewModel
        {
            Id = cartPosition.Id,
            Name = cartPosition.Name,
            Product = ToViewModel(cartPosition.Product),
            Quantity = cartPosition.Quantity,
            
        };
    }
    public static CartPosition ToDBModel(this CartPositionViewModel cartPositionViewModel)
    {
        return new CartPosition
        {
            Id = cartPositionViewModel.Id,
            Name = cartPositionViewModel.Name,
            Product = ToDBModel(cartPositionViewModel.Product),
            Quantity = cartPositionViewModel.Quantity
        };
    }
    public static CartViewModel ToViewModel(this Cart cartDB)
    {
        if (cartDB == null)
            return null;
        CartViewModel newCart = new CartViewModel();
        newCart.Id = cartDB.Id;
        newCart.UserId = cartDB.UserId;
        List<CartPositionViewModel> positions = new List<CartPositionViewModel>();
        foreach (var position in cartDB.Positions)
        {
            positions.Add(ToViewModel(position));
        }
        newCart.Positions = positions;
        return newCart;
    }

    public static Cart ToDBModel(this CartViewModel cartViewModel)
    {
        Cart newCart = new Cart();
        newCart.Id = cartViewModel.Id;
        newCart.UserId = cartViewModel.UserId;
        List<CartPosition> positions = new List<CartPosition>();
        foreach (var position in cartViewModel.Positions)
        {
            positions.Add(ToDBModel(position));
        }
        newCart.Positions = positions;
        return newCart;
    }
    public static UserViewModel ToViewModel(this User user)
    {
        return new UserViewModel()
        {
            Email = user.Email,
            UserName = user.UserName,
            PhoneNumber =  user.PhoneNumber
        };
    }
}
