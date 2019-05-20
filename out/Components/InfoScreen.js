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
        const infodata = this.props.infodata;
        console.log('infodata');
        console.log(infodata);
        return (
            <>
                <View style={styles.container}>
                    <StatusBar hidden={true} />
                    <Text style={styles.title}>{data}</Text>
                    <ScrollView style={styles.textContainer}>
                        <Text style={styles.text}>{infodata.Info}</Text>
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

