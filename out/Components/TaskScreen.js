import React, { Component } from 'react';
import { Platform, StyleSheet, Text, View, StatusBar, Alert, Button, FlatList, Image, TouchableOpacity } from 'react-native';
import { Actions } from 'react-native-router-flux';
import { UnityModule, MessageHandler } from 'react-native-unity-view';
import UnityView from './UnityView';

class TaskScreen extends Component {
    render() {
        const infodata = this.props.info;
        let tasks = [];
        for (var i in infodata) {
            tasks.push({ 'Task': i })
        };
        return [
            <>
                <View style={styles.container}>
                    <Text style={styles.title}>Tasks</Text>
                    <FlatList
                        data={tasks}
                        renderItem={({ item }) => (
                            <TouchableOpacity onPress={() => Actions.Info({ data: item.Task, infodata: infodata[item.Task] })}>
                                <View style={styles.card}>
                                    <Image
                                        style={styles.iconLeft}
                                        source={require('../Icons/cube.png')} />
                                    <View>
                                        <View>
                                            <Text style={styles.cardTitle}>{item.Task}</Text>
                                        </View>
                                    </View>
                                    <Image
                                        style={styles.iconRight}
                                        source={require('../Icons/right-arrow.png')} />

                                </View>
                            </TouchableOpacity>
                        )}

                    />
                </View>
                <UnityView />
            </>
        ]
    }
}

export default TaskScreen;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#f5f5f5',
        zIndex: 100,
        position: 'absolute',
        top: 0,
        bottom: 0,
        left: 0,
        right: 0,
    },
    title: {
        fontSize: 30,
        padding: 20,
        color: '#000'
    },
    list: {
        flex: 1,
        paddingTop: 5,
    },
    card: {
        backgroundColor: '#fff',
        flex: 1,
        padding: 10,
        paddingTop: 30,
        paddingBottom: 30,
        margin: 10,
        marginTop: 5,
        marginBottom: 5,
        flex: 0,
        flexDirection: 'row',
        borderRadius: 10
    },
    cardTitle: {
        fontSize: 20,
        fontWeight: '300',
        marginLeft: 7,
        textAlign: 'left',
    },
    iconLeft: {
        width: 30,
        height: 30,
        marginLeft: 15,
        marginRight: 15
    },
    iconRight: {
        width: 30,
        height: 30,
        marginLeft: 'auto',
        marginRight: 15
    },
})