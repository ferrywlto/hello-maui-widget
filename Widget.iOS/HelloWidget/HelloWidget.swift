import WidgetKit
import SwiftUI

struct DemoEntry: TimelineEntry {
    let date: Date
    let hello: String
}

struct DemoProvider: TimelineProvider {
    func placeholder(in context: Context) -> DemoEntry {
        DemoEntry(date: .now, hello: "—")
    }

    func getSnapshot(in context: Context, completion: @escaping (DemoEntry) -> ()) {
        completion(DemoEntry(date: .now, hello: readHello()))
    }

    func getTimeline(in context: Context, completion: @escaping (Timeline<DemoEntry>) -> ()) {
        let entry = DemoEntry(date: .now, hello: readHello())
        // Suggest a short reload; OS may throttle
        let next = Calendar.current.date(byAdding: .minute, value: 15, to: .now)!
        completion(Timeline(entries: [entry], policy: .after(next)))
    }

    private func readHello() -> String {
        let defaults = UserDefaults(suiteName: "app-group.ferry.hello-maui-widget")
        return defaults?.string(forKey: "helloValue") ?? "not set"
    }
}

struct DemoWidgetView: View {
    let entry: DemoProvider.Entry
    var body: some View {
        VStack(spacing: 6) {
            Text("Hello from App Group:")
                .font(.caption)
            Text(entry.hello).font(.headline).minimumScaleFactor(0.5)
            Text("Tap to open app").font(.caption2)
        }
        .padding(6)
        .widgetURL(URL(string: "myapp://hello")!) // deep link to MAUI app
    }
}

struct HelloWidget: Widget {
    let kind = "DemoWidget"
    var body: some WidgetConfiguration {
        StaticConfiguration(kind: kind, provider: DemoProvider()) { entry in
            DemoWidgetView(entry: entry)
                .containerBackground(.fill.tertiary, for: .widget)
        }
        .configurationDisplayName("Hello PoC")
        .description("Shows a value from App Group and deep-links to the app.")
        .supportedFamilies([.systemSmall])
    }
}
