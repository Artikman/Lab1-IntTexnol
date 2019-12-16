package by.company.sapriko;

import com.rabbitmq.client.Channel;
import com.rabbitmq.client.Connection;
import com.rabbitmq.client.ConnectionFactory;

import java.nio.charset.StandardCharsets;

/*
 * Этот класс используется для отправки текстового сообщения в очередь.
 */
public class MessageSender {

    private final static String QUEUE_NAME = "Message_Queue";

    public static void main(String[] args) {

        ConnectionFactory factory = new ConnectionFactory();

        /*
            Здесь я подключаемся к брокеру на локальной машине - следовательно
            к localhost. Можно подключиться также к брокеру другого компьютера,
            просто указав здесь его имя или IP-адрес.
         */
        factory.setHost("localhost");

        try (
                Connection connection = factory.newConnection();
                Channel channel = connection.createChannel())
        {
            //конфигурирую точку обмена, очередь и связь между ними
            channel.queueDeclare(QUEUE_NAME, false, false, false, null);
            String message = "Hello world!";
            //принимаю byte[] в качестве тела сообщения и копируем эти параметры в один массив
            channel.basicPublish("", QUEUE_NAME, null, message.getBytes(StandardCharsets.UTF_8));
            System.out.println(" [x] Sent '" + message + "'");
        }
        catch (Exception exe) {
            exe.printStackTrace();
        }
    }
}