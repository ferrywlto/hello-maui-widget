//
//  HelloWidgetBridge.swift
//  HelloWidgetBridge
//
//  Created by Ferry To on 12/11/2025.
//
import Foundation
import WidgetKit

@_cdecl("HelloWidgetBridge_SayHello")
public func HelloWorldBridge_SayHello() {
    NSLog("👋 Hello World from Swift!")
    WidgetCenter.shared.reloadAllTimelines()
}
