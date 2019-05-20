import React, { Component } from 'react';
import { Text, View, StyleSheet, Button } from 'react-native';
import { UnityModule, MessageHandler } from 'react-native-unity-view';
import UnityView from './UnityView';
import { Actions } from 'react-native-router-flux';


export default class Machine extends Component {
    componentDidMount() {
        console.log(this.props.data)
        UnityModule.postMessageToUnityManager({
            name: this.props.data,
            data: '',
        });
    }

    onLayerClickAll() {
        console.log('All Layers')
        UnityModule.postMessageToUnityManager({
            name: 'All Layers',
            data: '',
        });
    }

    onLayerClick1() {
        console.log('Layer 1')
        UnityModule.postMessageToUnityManager({
            name: 'Layer 1',
            data: '',
        });
    }

    onLayerClick2() {
        console.log('Layer 2')
        UnityModule.postMessageToUnityManager({
            name: 'Layer 2',
            data: '',
        });
    }

    onBackToMachine() {
        console.log('Return to machine')
        UnityModule.postMessageToUnityManager({
            name: 'Return to machine',
            data: '',
        });
    }

    render() {
        return (
            <View style={{ flex: 1 }}>
                <View style={styles.container}>
                    <UnityView />
                    <View style={styles.buttonContainer}>
                        <Button title="All Layers" color='#c42828' onPress={this.onLayerClickAll} />
                        <Button title="Layer 1" color='#c42828' onPress={this.onLayerClick1} />
                        <Button title="Layer 2" color='#c42828' onPress={this.onLayerClick2} />
                    </View>
                </View>
            </View>
        )
    }
}

const styles = StyleSheet.create({
    container: {
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
    welcome: {
        fontSize: 20,
        textAlign: 'center',
        margin: 10,
    },
    buttonContainer: {
        margin: 10
    },
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
        zIndex: 4
    },
});