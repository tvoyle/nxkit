﻿module NXKit.Web {

    export interface INodeMethodInvokedEvent extends IEvent {
        add(listener: () => void): void;
        remove(listener: () => void): void;
        trigger(...a: any[]): void;
        add(listener: (node: Node, $interface: Interface, method: Method, params: any) => void): void;
        remove(listener: (node: Node, $interface: Interface, method: Method, params: any) => void): void;
        trigger(node: Node, $interface: Interface, method: Method, params: any): void;
    }

}