package by.company.sapriko;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;
import com.rabbitmq.client.DeliverCallback;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.util.concurrent.TimeoutException;

/*
 * Этот класс используется для получения текстового сообщения в очередь.
 */
public class MessageReceiver
{
    private final static String QUEUE_NAME = "Message_Queue";

    public static void main(String[] args) throws IOException, TimeoutException
    {
        ConnectionFactory factory = new ConnectionFactory();
        /*
            Здесь я подключаем к брокеру на локальной машине - следовательно
            к localhost. Можно подключиться также к брокеру другого компьютера,
            просто указав здесь его имя или IP-адрес.
         */
        factory.setHost("localhost");
        Connection connection = factory.newConnection();
        Channel channel = connection.createChannel();

        //конфигурирую точку обмена, очередь и связь между ними
        channel.queueDeclare(QUEUE_NAME, false, false, false, null);
        System.out.println(" [*] Waiting for messages. To exit press CTRL+C");

        /*
            Здесь я говорю серверу доставить нам сообщения из очереди.
            Поскольку он отправляет нам сообщения асинхронно, я предоставляю
            обратный вызов в форме объекта, который буферизует сообщения до тех пор,
            пока я не буду готов их использовать.
            Это то, что делает подкласс этот.
         */
        DeliverCallback deliverCallback = (consumerTag, delivery) -> {
            String message = new String(delivery.getBody(), StandardCharsets.UTF_8);
            System.out.println(" [x] Received '" + message + "'");
        };
        //регистрирую basicConsume для прослушивания определенной очереди
        channel.basicConsume(QUEUE_NAME, true, deliverCallback,
                consumerTag -> {
        });
    }
}