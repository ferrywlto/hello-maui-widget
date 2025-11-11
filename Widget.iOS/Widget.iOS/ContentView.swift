//
//  ContentView.swift
//  Widget.iOS
//
//  Created by Ferry To on 10/11/2025.
//

import SwiftUI
import WidgetKit

struct ContentView: View {
    @State private var count = 0
    @State private var lastValue: String = UserDefaults(suiteName: "group.ferry.hello-maui-widget")?.string(forKey: "helloValue") ?? "none"
    
    var body: some View {
        VStack(spacing: 16) {
            Text("Last value: \(lastValue)")
                .font(.headline)
            
            Button("Write & Reload") {
                count += 1
                let val = "Hello #\(count) at \(Date().formatted())"
                if let defaults = UserDefaults(suiteName: "group.ferry.hello-maui-widget") {
                    defaults.set(val, forKey: "helloValue")
                }
                lastValue = val
                WidgetCenter.shared.reloadAllTimelines()
            }
            .buttonStyle(.borderedProminent)
        }
        .padding()
    }
}

#Preview {
   ContentView()
}
