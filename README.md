# Quartz Job Scheduling with MassTransit and RabbitMQ

This repository contains a demo project that integrates [Quartz.NET](https://www.quartz-scheduler.net/) for job scheduling with [MassTransit](https://masstransit.io/) and RabbitMQ for message-based communication. The project demonstrates how to use Quartz to schedule jobs and how MassTransit can handle message delivery and processing efficiently through RabbitMQ.

## Features

- **Quartz.NET Integration:** Schedule recurring jobs using Quartz's robust job scheduling capabilities.
- **MassTransit with RabbitMQ:** Use MassTransit to handle messaging, allowing jobs to communicate with other services or processes asynchronously.
- **Seamless Messaging:** Set up and configure RabbitMQ to work as the message broker, ensuring reliable and scalable message delivery.

## Getting Started

To run this demo, ensure you have RabbitMQ installed and running. Follow the official [MassTransit documentation](https://masstransit.io/documentation) and [Quartz.NET documentation](https://www.quartz-scheduler.net/documentation/) for detailed setup instructions and additional configuration options.

### Prerequisites

- .NET Core 8 or higher
- RabbitMQ
- MassTransit
- Quartz.NET

### Installation

. Update the `appsettings.json` with your RabbitMQ configuration:
    ```json
    "RabbitMq": {
      "Host": "localhost",
      "Username": "guest",
      "Password": "guest"
    }
    ```


### Usage

- **Quartz Job Scheduling:** The project demonstrates a simple job that gets executed periodically based on the Quartz scheduler's configuration.
- **MassTransit Messaging:** Once the job is triggered, a message is sent via MassTransit and handled by a consumer.

## Documentation

- [MassTransit Documentation](https://masstransit.io/documentation)
- [Quartz.NET Documentation](https://www.quartz-scheduler.net/documentation/)

## Contributing

Feel free to fork this repository, submit issues, or create pull requests. Contributions are always welcome!

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
