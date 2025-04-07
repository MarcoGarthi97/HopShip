# 🐇 Order Processing with RabbitMQ, Docker, and PostgreSQL

This is a study project developed in **C#** to explore and understand how **RabbitMQ** works within a microservices architecture.

## 🚀 Description

The application simulates an order management system with a pipeline composed of multiple services communicating through **RabbitMQ**. Each service performs a specific step in the process:

1. An **API** receives a request to create an order.
2. The order is **saved in a PostgreSQL database**.
3. The API sends a message containing the order ID to a RabbitMQ queue.
4. The **Order Service** receives the message, processes the order, and publishes a new message.
5. The **Payment Service** receives the new message, simulates payment, and sends another message.
6. The **Shipping Service** receives the final message and completes the flow by simulating shipment.

Everything is orchestrated using **Docker**, including RabbitMQ and PostgreSQL services.

## 🐳 Architecture

```mermaid
flowchart TD
    
    B[(Orders Table)]

    C[RabbitMQ]

    A[REST API - Create Order] -->|Save Order| B
    A -->|Send Order ID| C

    C --> D[Order Service]
    D -->|Read/Update Order| B
    D -->|Send Order ID| C

    C --> E[Payment Service]
    E -->|Read/Update Order| B
    E -->|Send Order ID| C

    C --> F[Shipping Service]
    F -->|Read/Update Order| B
