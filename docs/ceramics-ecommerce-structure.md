# 🏺 Ceramics E-commerce Backend Microservices Structure

## 📁 Project Root Structure

```
CeramicsShop-Backend/
├── 📁 src/
│   ├── 📁 ApiGateway/
│   │   └── 📁 CeramicsShop.Gateway/
│   ├── 📁 Services/
│   │   ├── 📁 Product/
│   │   │   └── 📁 CeramicsShop.Product.API/
│   │   ├── 📁 Order/
│   │   │   └── 📁 CeramicsShop.Order.API/
│   │   ├── 📁 Identity/
│   │   │   └── 📁 CeramicsShop.Identity.API/
│   │   └── 📁 Notification/
│   │       └── 📁 CeramicsShop.Notification.API/
│   └── 📁 Shared/
│       ├── 📁 CeramicsShop.Shared.Common/
│       ├── 📁 CeramicsShop.Shared.EventBus/
│       └── 📁 CeramicsShop.Shared.Payment/
├── 📁 docker/
├── 📁 scripts/
└── 📁 docs/
```

## 🌐 API Gateway - Ocelot

```
src/ApiGateway/CeramicsShop.Gateway/
├── 📁 Configuration/
│   ├── ocelot.json
│   ├── ocelot.Development.json
│   └── ocelot.Production.json
├── 📁 Middleware/
│   ├── JwtAuthenticationMiddleware.cs
│   ├── RequestLoggingMiddleware.cs
│   └── ExceptionMiddleware.cs
├── 📁 Extensions/
│   └── ServiceCollectionExtensions.cs
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── appsettings.Production.json
└── Dockerfile
```

### Ocelot Gateway Configuration
```json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/api/products/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "ceramics-product-api",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/api/products/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    },
    {
      "DownstreamPathTemplate": "/api/orders/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "ceramics-order-api",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/api/orders/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ],
      "AuthenticationOptions": {
        "AuthenticationProviderKey": "Bearer",
        "AllowedScopes": []
      }
    },
    {
      "DownstreamPathTemplate": "/api/auth/{everything}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "ceramics-identity-api",
          "Port": 80
        }
      ],
      "UpstreamPathTemplate": "/api/auth/{everything}",
      "UpstreamHttpMethod": [ "GET", "POST", "PUT", "DELETE" ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:5000"
  }
}
```

## 🏺 Product API Service

```
src/Services/Product/CeramicsShop.Product.API/
├── 📁 Controllers/
│   ├── ProductsController.cs
│   ├── CategoriesController.cs
│   └── MaterialsController.cs
├── 📁 Models/
│   ├── Product.cs
│   ├── Category.cs
│   ├── Material.cs
│   ├── ProductImage.cs
│   └── ProductSpecification.cs
├── 📁 Data/
│   ├── ProductContext.cs
│   ├── ProductSeed.cs
│   └── 📁 Migrations/
├── 📁 Services/
│   ├── IProductService.cs
│   ├── ProductService.cs
│   ├── ICategoryService.cs
│   └── CategoryService.cs
├── 📁 DTOs/
│   ├── ProductDto.cs
│   ├── CreateProductDto.cs
│   ├── UpdateProductDto.cs
│   ├── CategoryDto.cs
│   ├── MaterialDto.cs
│   └── ProductFilterDto.cs
├── 📁 Repositories/
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── ICategoryRepository.cs
│   └── CategoryRepository.cs
├── 📁 Mappings/
│   └── AutoMapperProfile.cs
├── 📁 Validators/
│   ├── CreateProductValidator.cs
│   └── UpdateProductValidator.cs
├── Program.cs
├── appsettings.json
└── Dockerfile
```

### Product Models
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int MaterialId { get; set; }
    public Material Material { get; set; }
    public string Dimensions { get; set; } // "20x15x10 cm"
    public decimal Weight { get; set; } // in kg
    public string Color { get; set; }
    public bool IsHandmade { get; set; }
    public string Glazing { get; set; } // "Matte", "Glossy", "Textured"
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<ProductImage> Images { get; set; }
    public ProductSpecification Specification { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } // "Bowls", "Vases", "Plates", "Cups"
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public List<Product> Products { get; set; }
}

public class Material
{
    public int Id { get; set; }
    public string Name { get; set; } // "Porcelain", "Stoneware", "Earthenware"
    public string Description { get; set; }
    public List<Product> Products { get; set; }
}
```

## 📦 Order API Service

```
src/Services/Order/CeramicsShop.Order.API/
├── 📁 Controllers/
│   ├── OrdersController.cs
│   └── PaymentsController.cs
├── 📁 Models/
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── OrderStatus.cs
│   ├── ShippingAddress.cs
│   └── PaymentInfo.cs
├── 📁 Data/
│   ├── OrderContext.cs
│   └── 📁 Migrations/
├── 📁 Services/
│   ├── IOrderService.cs
│   ├── OrderService.cs
│   ├── IPaymentService.cs
│   ├── StripePaymentService.cs
│   └── IEventBusService.cs
├── 📁 DTOs/
│   ├── OrderDto.cs
│   ├── CreateOrderDto.cs
│   ├── OrderItemDto.cs
│   ├── PaymentRequestDto.cs
│   └── PaymentResponseDto.cs
├── 📁 Events/
│   ├── OrderCreatedEvent.cs
│   ├── OrderPaidEvent.cs
│   ├── OrderShippedEvent.cs
│   └── PaymentCompletedEvent.cs
├── 📁 EventHandlers/
│   ├── OrderCreatedEventHandler.cs
│   └── PaymentCompletedEventHandler.cs
├── 📁 Repositories/
│   ├── IOrderRepository.cs
│   └── OrderRepository.cs
├── 📁 Validators/
│   └── CreateOrderValidator.cs
├── Program.cs
├── appsettings.json
└── Dockerfile
```

### Order Models
```csharp
public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public string Notes { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal TaxAmount { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}

public enum OrderStatus
{
    Pending = 1,
    PaymentProcessing = 2,
    Paid = 3,
    Processing = 4,
    Shipped = 5,
    Delivered = 6,
    Cancelled = 7,
    Refunded = 8
}
```

## 🔐 Identity API Service

```
src/Services/Identity/CeramicsShop.Identity.API/
├── 📁 Controllers/
│   ├── AuthController.cs
│   └── UsersController.cs
├── 📁 Models/
│   ├── User.cs
│   ├── Role.cs
│   ├── RefreshToken.cs
│   └── UserProfile.cs
├── 📁 Data/
│   ├── IdentityContext.cs
│   ├── IdentitySeed.cs
│   └── 📁 Migrations/
├── 📁 Services/
│   ├── IAuthService.cs
│   ├── AuthService.cs
│   ├── ITokenService.cs
│   ├── TokenService.cs
│   ├── IUserService.cs
│   └── UserService.cs
├── 📁 DTOs/
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── TokenResponseDto.cs
│   ├── UserDto.cs
│   └── UpdateProfileDto.cs
├── 📁 Validators/
│   ├── LoginValidator.cs
│   ├── RegisterValidator.cs
│   └── UpdateProfileValidator.cs
├── 📁 Repositories/
│   ├── IUserRepository.cs
│   └── UserRepository.cs
├── Program.cs
├── appsettings.json
└── Dockerfile
```

### Identity Models
```csharp
public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string Role { get; set; } = "Customer";
    public UserProfile Profile { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; }
}

public class UserProfile
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
}
```

## 📧 Notification API Service

```
src/Services/Notification/CeramicsShop.Notification.API/
├── 📁 Controllers/
│   └── NotificationsController.cs
├── 📁 Models/
│   ├── EmailTemplate.cs
│   ├── NotificationLog.cs
│   └── EmailConfiguration.cs
├── 📁 Data/
│   ├── NotificationContext.cs
│   └── 📁 Migrations/
├── 📁 Services/
│   ├── IEmailService.cs
│   ├── EmailService.cs
│   ├── ITemplateService.cs
│   └── TemplateService.cs
├── 📁 Templates/
│   ├── order-confirmation.html
│   ├── payment-success.html
│   ├── order-shipped.html
│   └── welcome.html
├── 📁 DTOs/
│   ├── SendEmailDto.cs
│   ├── EmailTemplateDto.cs
│   └── NotificationDto.cs
├── 📁 EventHandlers/
│   ├── OrderCreatedEventHandler.cs
│   ├── PaymentCompletedEventHandler.cs
│   └── OrderShippedEventHandler.cs
├── 📁 Repositories/
│   ├── INotificationRepository.cs
│   └── NotificationRepository.cs
├── Program.cs
├── appsettings.json
└── Dockerfile
```

### Email Templates
```html
<!-- order-confirmation.html -->
<!DOCTYPE html>
<html>
<head>
    <title>Order Confirmation - Ceramics Shop</title>
</head>
<body>
    <h1>Thank you for your order!</h1>
    <p>Dear {{CustomerName}},</p>
    <p>Your order #{{OrderNumber}} has been received and is being processed.</p>
    
    <h3>Order Details:</h3>
    <table>
        <tr>
            <th>Product</th>
            <th>Quantity</th>
            <th>Price</th>
        </tr>
        {{#each OrderItems}}
        <tr>
            <td>{{ProductName}}</td>
            <td>{{Quantity}}</td>
            <td>${{UnitPrice}}</td>
        </tr>
        {{/each}}
    </table>
    
    <p><strong>Total: ${{TotalAmount}}</strong></p>
    <p>We'll notify you when your beautiful ceramics are ready to ship!</p>
</body>
</html>
```

## 🔗 Shared Libraries

```
src/Shared/CeramicsShop.Shared.Common/
├── 📁 Models/
│   ├── BaseEntity.cs
│   ├── AuditableEntity.cs
│   ├── ApiResponse.cs
│   └── PagedResult.cs
├── 📁 Extensions/
│   ├── ServiceCollectionExtensions.cs
│   └── StringExtensions.cs
├── 📁 Exceptions/
│   ├── BusinessException.cs
│   ├── NotFoundException.cs
│   └── ValidationException.cs
├── 📁 Constants/
│   ├── AppConstants.cs
│   └── EmailConstants.cs
└── 📁 Helpers/
    ├── PasswordHelper.cs
    └── FileHelper.cs
```

```
src/Shared/CeramicsShop.Shared.EventBus/
├── 📁 Abstractions/
│   ├── IEventBus.cs
│   ├── IIntegrationEvent.cs
│   └── IIntegrationEventHandler.cs
├── 📁 Events/
│   ├── IntegrationEvent.cs
│   ├── OrderCreatedIntegrationEvent.cs
│   ├── PaymentCompletedIntegrationEvent.cs
│   └── OrderShippedIntegrationEvent.cs
├── 📁 RabbitMQ/
│   ├── RabbitMQEventBus.cs
│   ├── RabbitMQConnection.cs
│   └── RabbitMQConfiguration.cs
└── 📁 Extensions/
    └── EventBusExtensions.cs
```

```
src/Shared/CeramicsShop.Shared.Payment/
├── 📁 Models/
│   ├── PaymentRequest.cs
│   ├── PaymentResponse.cs
│   └── StripeSettings.cs
├── 📁 Services/
│   ├── IPaymentService.cs
│   └── StripePaymentService.cs
└── 📁 Extensions/
    └── PaymentExtensions.cs
```

## 🐳 Docker Configuration

```
docker/
├── docker-compose.yml
├── docker-compose.override.yml
├── .env
└── 📁 databases/
    └── init-databases.sql
```

### Docker Compose Configuration
```yaml
version: '3.8'

services:
  # API Gateway
  ceramics-gateway:
    image: ceramics-gateway:latest
    container_name: ceramics-gateway
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
    depends_on:
      - ceramics-product-api
      - ceramics-order-api
      - ceramics-identity-api
      - ceramics-notification-api

  # Product API
  ceramics-product-api:
    image: ceramics-product-api:latest
    container_name: ceramics-product-api
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=CeramicsProductDb;User Id=sa;Password=YourStrong@Passw0rd;
    depends_on:
      - sqlserver

  # Order API
  ceramics-order-api:
    image: ceramics-order-api:latest
    container_name: ceramics-order-api
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=CeramicsOrderDb;User Id=sa;Password=YourStrong@Passw0rd;
      - StripeSettings__SecretKey=${STRIPE_SECRET_KEY}
      - RabbitMQ__ConnectionString=amqp://guest:guest@rabbitmq:5672/
    depends_on:
      - sqlserver
      - rabbitmq

  # Identity API
  ceramics-identity-api:
    image: ceramics-identity-api:latest
    container_name: ceramics-identity-api
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=CeramicsIdentityDb;User Id=sa;Password=YourStrong@Passw0rd;
      - JwtSettings__Secret=${JWT_SECRET}
    depends_on:
      - sqlserver

  # Notification API
  ceramics-notification-api:
    image: ceramics-notification-api:latest
    container_name: ceramics-notification-api
    environment:
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=CeramicsNotificationDb;User Id=sa;Password=YourStrong@Passw0rd;
      - EmailSettings__SmtpServer=${SMTP_SERVER}
      - EmailSettings__SmtpPort=${SMTP_PORT}
      - EmailSettings__Username=${SMTP_USERNAME}
      - EmailSettings__Password=${SMTP_PASSWORD}
      - RabbitMQ__ConnectionString=amqp://guest:guest@rabbitmq:5672/
    depends_on:
      - sqlserver
      - rabbitmq

  # SQL Server
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: ceramics-sqlserver
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql

  # RabbitMQ
  rabbitmq:
    image: rabbitmq:3-management
    container_name: ceramics-rabbitmq
    ports:
      - "5672:5672"
      - "15672:15672"
    environment:
      - RABBITMQ_DEFAULT_USER=guest
      - RABBITMQ_DEFAULT_PASS=guest
    volumes:
      - rabbitmq_data:/var/lib/rabbitmq

volumes:
  sqlserver_data:
  rabbitmq_data:
```

## 🔄 Event Flow & RabbitMQ Queues

### Queue Configuration
```json
{
  "Queues": [
    {
      "Name": "order.created",
      "Durable": true,
      "AutoDelete": false
    },
    {
      "Name": "payment.completed",
      "Durable": true,
      "AutoDelete": false
    },
    {
      "Name": "order.shipped",
      "Durable": true,
      "AutoDelete": false
    },
    {
      "Name": "notification.email",
      "Durable": true,
      "AutoDelete": false
    }
  ]
}
```

### Payment Processing Flow
1. **Order Created** → Queue: `order.created`
2. **Payment Processing** → Stripe API → Queue: `payment.processing`
3. **Payment Success** → Queue: `payment.completed` → Email Notification
4. **Payment Failed** → Queue: `payment.failed` → Email Notification

## 🗄️ Database Schemas

### Products Database (CeramicsProductDb)
- Products
- Categories  
- Materials
- ProductImages
- ProductSpecifications

### Orders Database (CeramicsOrderDb)
- Orders
- OrderItems
- ShippingAddresses
- PaymentInfos

### Identity Database (CeramicsIdentityDb)
- Users
- UserProfiles
- RefreshTokens
- Roles

### Notifications Database (CeramicsNotificationDb)
- EmailTemplates
- NotificationLogs
- EmailConfigurations

Cấu trúc này đảm bảo:
- ✅ Tách biệt database cho từng service
- ✅ JWT Authentication/Authorization
- ✅ Stripe Payment Integration
- ✅ RabbitMQ Event-driven Architecture
- ✅ Email Notifications
- ✅ Ocelot API Gateway
- ✅ Docker Containerization
- ✅ Microservices Best Practices