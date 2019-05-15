import React, { Component } from 'react';
import { BackHandler, Image } from 'react-native';
import { Router, Scene, Actions } from 'react-native-router-flux';
import { UnityModule, MessageHandler } from 'react-native-unity-view';

import LoginScreen from './Components/LoginScreen';
import TaskScreen from './Components/TaskScreen';
import InfoScreen from './Components/InfoScreen';
import MachineScreen from './Components/MachineScreen';
import MachinePartScreen from './Components/MachinePartScreen';
import MachineInfoScreen from './Components/MachineInfoScreen';

console.disableYellowBox = true;

export default class App extends Component {
    Explode() {
        console.log('Explode')
        UnityModule.postMessageToUnityManager({
            name: 'Explode',
            data: '',
        });
    }

    Refresh() {
        console.log('Refresh')
    }

    render() {
        return (
            <Router navigationBarStyle={{ backgroundColor: '#c42828' }}>

                <Scene key='root'>

                    <Scene
                        key='Login'
                        component={LoginScreen}
                        title='Login'
                        initial
                        hideNavBar
                    />
                    <Scene
                        key='Task'
                        component={TaskScreen}
                        title='Task'

                    />
                    <Scene
                        key='Info'
                        component={InfoScreen}
                        title='Info'
                    />
                    <Scene
                        key='Machine'
                        component={MachineScreen}
                        title='Machine'
                    />
                    <Scene
                        key='MachinePart'
                        component={MachinePartScreen}
                        title='MachinePart'
                        onRight={this.Explode}
                        rightTitle={'Explode'}
                        rightButtonTextStyle={{ color: 'black' }}
                    />
                    <Scene
                        key='MachineInfo'
                        component={MachineInfoScreen}
                        title='MachineInfo'
                        onRight={this.Refresh}
                        rightTitle={'Refresh'}
                        rightButtonTextStyle={{ color: 'black' }}
                    />


                </Scene>

            </Router>
        )
    }
};

