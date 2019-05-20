import React, { Component } from 'react';
import {
    View,
    Text,
    StyleSheet,
    ScrollView,
    TouchableOpacity,
    StatusBar,
    Button
} from 'react-native';
import { Actions } from 'react-native-router-flux';
import { UnityModule, MessageHandler } from 'react-native-unity-view';

export default class Info extends Component {
    componentDidMount() {
        console.log('Reset')
        UnityModule.postMessageToUnityManager({
            name: 'Reset',
            data: '',
        });
    }
    render() {
        const data = this.props.data;
        return (
            <>
                <View style={styles.container}>
                    <StatusBar hidden={true} />
                    <Text style={styles.title}>{data}</Text>
                    <ScrollView style={styles.textContainer}>
                        <Text style={styles.text}>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent varius est in massa interdum vestibulum. Duis fermentum felis et mollis maximus. Vivamus eu lectus fermentum, auctor lorem nec, pretium mi. Duis laoreet velit vel sem placerat, tempus aliquam sem commodo. Sed iaculis lacinia ex a pulvinar. Donec vel blandit elit. Integer id lobortis enim, at suscipit erat. Sed in purus lobortis, varius augue a, fermentum tellus. Quisque laoreet, nibh nec tempus malesuada, mauris lectus iaculis risus, id cursus nisl massa sit amet nulla. Vestibulum leo ex, porta ut tincidunt et, vulputate id lorem.Duis feugiat lectus quis justo rhoncus, vel accumsan tellus suscipit. Mauris ornare nunc metus, sit amet ullamcorper nibh fermentum non. Duis tempus sollicitudin leo ut auctor. Suspendisse et sem consectetur tortor pretium euismod et malesuada augue. Aliquam sit amet eros quis nisi ullamcorper varius ac et nulla. Quisque sit amet erat metus. Nulla congue mi at nibh molestie ultrices. Praesent at ipsum ligula. Nulla molestie venenatis felis non faucibus. Nullam in neque hendrerit, sodales augue nec, mattis ipsum. Nunc nec feugiat nulla. Sed dui orci, venenatis non eros ut, tincidunt ultrices elit.Vivamus eu nisi ut mauris suscipit porttitor. Vivamus eleifend fringilla ante, in vulputate lorem. Vestibulum ornare sodales metus. Nullam bibendum quam vitae dolor malesuada vulputate. Morbi faucibus nec arcu nec gravida. Integer sed neque neque. Aenean vitae magna ut lacus condimentum condimentum. Nulla tempor non dui eu accumsan.Nullam imperdiet nec elit eu pharetra. In nec risus ex. Donec in egestas arcu. Nulla facilisi. Pellentesque suscipit ante id viverra dapibus. Sed imperdiet velit in libero laoreet aliquam. Integer maximus aliquam odio, sed tempor felis fringilla a. Sed consectetur nulla nunc, semper tincidunt justo dignissim eget. Praesent vestibulum sapien eget metus vestibulum, sed dignissim sem rutrum. Phasellus blandit at metus sit amet aliquet. Donec dignissim scelerisque nulla et mollis. Vivamus venenatis placerat gravida.Maecenas tincidunt tempus pretium. Proin bibendum faucibus nibh at lobortis. Aliquam nec lorem vel tortor ultrices sagittis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec non ante pulvinar, placerat tellus eu, ornare nunc. Praesent ante magna, porta ut leo et, bibendum fermentum eros. Cras maximus magna a commodo efficitur. Mauris commodo auctor tristique. Integer congue, sem ut euismod ultrices, neque sem scelerisque turpis, in venenatis risus enim sit amet mauris. Nunc pulvinar molestie ante, in consectetur urna pretium et. Donec ullamcorper, nisl ut convallis dignissim, enim neque facilisis urna, in ultricies metus diam nec sem.</Text>
                    </ScrollView>
                    <TouchableOpacity style={styles.buttonContainer}>
                        <Text style={styles.buttonText} onPress={() => Actions.Machine({ data: data })}> START</Text>
                    </TouchableOpacity>
                </View>
            </>
        )
    }
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#f5f5f5'
    },
    title: {
        fontSize: 30,
        padding: 20,
        color: '#000'
    },
    textContainer: {
        padding: 20,
        backgroundColor: '#fff',
        marginLeft: 20,
        marginRight: 20,
        marginBottom: 30
    },
    text: {
        textAlign: 'left',
        fontSize: 20,
    },
    buttonContainer: {
        backgroundColor: '#c42828',
        paddingVertical: 15,
        width: 130,
        alignSelf: 'center',
        borderRadius: 30,
        marginBottom: 30
    },
    buttonText: {
        color: '#fff',
        textAlign: 'center',
        fontWeight: '700',
    },
});

