import React, { Component } from 'react';
import {
    Text,
    View,
    StyleSheet,
    Button,
    Alert,
    StatusBar,
    FlatList,
    Dimensions,
} from 'react-native';
import UnityView from './UnityView';

export default class MachineInfo extends Component {
    render() {
        return (
            <View style={{ flex: 1 }}>
                <UnityView />
                <View style={styles.boxContainer}>
                    <View style={styles.textBox}>
                        <View style={styles.textBoxText}>
                            <Text style={styles.title}>Info Part</Text>
                            <FlatList
                                style={{ marginTop: 80 }}
                                data={[
                                    { key: 'Devin', value: 'data' },
                                    { key: 'Jackson', value: 'data' },
                                    { key: 'James', value: 'data' },
                                    { key: 'Joel', value: 'data' },
                                ]}
                                renderItem={({ item }) => (
                                    <View style={styles.keyValueBox}>
                                        <View style={styles.keyBox}>
                                            <Text style={styles.key}>{item.key}</Text>
                                        </View>
                                        <View style={styles.valueBox}>
                                            <Text style={styles.value}>{item.value}</Text>
                                        </View>
                                    </View>
                                )}
                            />
                        </View>
                    </View>
                </View>

            </View>
        );
    }
}

const styles = StyleSheet.create({
    title: {
        position: 'absolute',
        fontSize: 30,
        paddingLeft: 50,
        paddingVertical: 20,
        color: '#000',
    },
    container: {
        flex: 1,
        position: 'absolute',
        top: 0,
        bottom: 0,
        left: 0,
        right: 0,
        flexDirection: 'row',
        alignItems: 'flex-start',
    },
    textBox: {
        flex: 1 / 3,
        alignSelf: 'stretch',
        backgroundColor: '#f5f5f5',
        borderTopLeftRadius: 15,
        borderTopRightRadius: 15,
    },
    textBoxText: {
        margin: 20,
    },
    boxContainer: {
        flex: 1,
        alignItems: 'flex-end',
        justifyContent: 'flex-end',
    },
    keyBox: {
        flex: 1,
        position: 'absolute',
        marginLeft: 15,
        alignSelf: 'flex-start',
    },
    key: {
        color: '#000',
        fontWeight: 'bold',
    },
    value: {
        color: '#000',
    },
    valueBox: {
        position: 'relative',
        alignSelf: 'flex-start',
        marginLeft: 150,
    },
    keyValueBox: {
        borderLeftWidth: 4,
        borderLeftColor: '#c42828',
        flexDirection: 'row',
        marginLeft: 35,
        justifyContent: 'flex-start',
    },
});
