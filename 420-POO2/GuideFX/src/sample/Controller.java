package sample;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.media.Media;
import javafx.scene.media.MediaPlayer;
import javafx.scene.media.MediaView;

import java.net.URL;
import java.util.ResourceBundle;

public class Controller implements Initializable {

    @FXML
    private MediaView mediaview;
    MediaPlayer mediaplayer;
    @FXML private Button playButton;
    private boolean playing=false;


    @Override
    public void initialize(URL url, ResourceBundle rb) {
        URL myUrl=Controller.class.getResource("");

        Media media= new Media(myUrl.toExternalForm());
        mediaplayer= new MediaPlayer(media);

        mediaview.setMediaPlayer(mediaplayer);
        mediaplayer.play();
    }

    public void playButtonClick(ActionEvent actionEvent) {
        playing = !playing;
        if (playing)
        {
            mediaplayer.play();
        }
        else
        {
            mediaplayer.stop();
        }
    }
}
