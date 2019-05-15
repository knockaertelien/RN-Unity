import React, { Component } from 'react';
import { Text, View, StyleSheet, Button, Alert, StatusBar } from 'react-native';
import UnityView, { UnityModule, MessageHandler } from 'react-native-unity-view';
import { Actions } from 'react-native-router-flux';

export default class UnityViewWrapper extends Component {
    constructor(props) {
        super(props);
        this.state = { isMounted: true }
    }

    onUnityMessage(MessageHandler) {
        console.log(MessageHandler.name)
        switch (MessageHandler.name) {
            case 'To part':
                Actions.MachinePart
                break;
            case 'To info':
                Actions.MachineInfo
                break;
            default:
                console.log('nope')
        }
    }

    // componentDidMount() {
    //     console.log('Component Did Mount')
    //     this.setState({ isMounted: true })
    // }


    // componentWillUnmount() {
    //     console.log('Component Did Unmount')
    //     this.setState({ isMounted: false })
    // }

    render() {
        const { isMounted } = this.state;
        return <>
            <StatusBar hidden={true} />
            {isMounted && <UnityView style={styles.view} onUnityMessage={this.onUnityMessage} />}
        </>
    }
}



const styles = StyleSheet.create({
    view: {
        flex: 1,
        position: 'absolute',
        top: 0,
        bottom: 0,
        left: 0,
        right: 0,
        justifyContent: 'space-between',
        alignItems: 'flex-end',
        backgroundColor: '#F5F5F5',
    },
});