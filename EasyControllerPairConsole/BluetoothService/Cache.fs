﻿namespace BluetoothService

open System.Collections.Generic
open InTheHand.Net.Sockets

module Cache =
    type Device =
        { DeviceKey: string
          DeviceData: BluetoothDeviceInfo }

    let cache = Dictionary<string, Device>()

    let cacheList() =
        cache
        |> Seq.map (|KeyValue|)
        |> Seq.toList
        |> List.map snd

    let existInCache deviceKey = cache.ContainsKey deviceKey

    // TODO: see if can handle errors
    let getDeviceFromCache deviceKey =
        match cache.TryGetValue deviceKey with
        | true, v -> v
        | false, _ -> Unchecked.defaultof<Device>

    let removeFromCache deviceKey = cache.Remove deviceKey

    let cacheIsEmpty () = cache.Count = 0

    let saveToCache (devices: Device list) =
        cache.Clear()

        devices
        |> List.iter (fun device -> cache.Add(device.DeviceKey, device))
