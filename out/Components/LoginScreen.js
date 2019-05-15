import React, { Component } from 'react';
import {
    View,
    Text,
    TextInput,
    TouchableOpacity,
    StyleSheet,
    Image,
    StatusBar
} from 'react-native';
import { Actions } from 'react-native-router-flux';
import { UnityModule, MessageHandler } from 'react-native-unity-view';

class TaskScreen extends Component {
    render() {
        return (
            <View behavior="padding" style={styles.container}>
                <StatusBar hidden={true} />

                <View style={styles.loginContainer}>
                    <Image
                        resizeMode="contain"
                        style={styles.logoDLW}
                        source={require('../Images/Logo_delaware_Pantone_print.png')}
                    />
                </View>
                <View style={styles.loginContainer}>
                    <View>
                        <View style={styles.container2}>


                            <View style={styles.input}>
                                <Image
                                    style={styles.icon}
                                    source={require('../Icons/user.png')}
                                />
                                <TextInput
                                    style={styles.inputText}
                                    autoCapitalize="none"
                                    onSubmitEditing={() => this.passwordInput.focus()}
                                    autoCorrect={false}
                                    keyboardType="email-address"
                                    returnKeyType="next"
                                    placeholder="Username"
                                    placeholderTextColor="#999999" />
                            </View>
                            <View style={styles.input}>
                                <Image
                                    style={styles.icon}
                                    source={require('../Icons/lock.png')}
                                />
                                <TextInput
                                    style={styles.inputText}
                                    returnKeyType="go"
                                    ref={input => (this.passwordInput = input)}
                                    placeholder="Password"
                                    placeholderTextColor="#999999"
                                    secureTextEntry
                                />
                            </View>
                            <TouchableOpacity
                                style={styles.buttonContainer}
                                onPress={Actions.Task}
                            >
                                <Text style={styles.buttonText}>LOGIN</Text>
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>
                <Image
                    resizeMode="contain"
                    style={styles.logoSAP}
                    source={require('../Images/SAP-Logo-PNG-Vector-Free-Download.png')}
                />
            </View>
        )
    }
}

export default TaskScreen;

const styles = StyleSheet.create({
    input: {
        height: 50,
        width: 300,
        backgroundColor: '#dddddd',
        marginBottom: 10,
        color: '#000',
        borderRadius: 30,
        flex: 0,
        flexDirection: 'row',
        alignItems: 'center'
    },
    inputText: {
        padding: 10,
    },
    buttonContainer: {
        backgroundColor: '#c42828',
        paddingVertical: 15,
        width: 130,
        alignSelf: 'center',
        borderRadius: 30,
    },
    buttonText: {
        color: '#fff',
        textAlign: 'center',
        fontWeight: '700',
    },
    container: {
        padding: 20,
        flex: 1,
        backgroundColor: '#f5f5f5',
    },
    container2: {
        paddingBottom: 100,
    },
    loginContainer: {
        alignItems: 'center',
        flexGrow: 1,
        justifyContent: 'center',
    },
    logoDLW: {
        position: 'absolute',
        width: 300,
        height: 60,
    },
    logoSAP: {
        width: 140,
        height: 50,
    },
    icon: {
        width: 18,
        height: 25,
        marginLeft: 15
    }
});