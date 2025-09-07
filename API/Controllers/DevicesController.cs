using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class DevicesController(AppDbContext context) : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Device>>> GetDevices()
        {
            List<Device> devices = await context.Devices.ToListAsync();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetDevice(int id)
        {
            Device device = await context.Devices.FindAsync(id);
            if (device == null) return NotFound();
            return Ok(device);
        }

        [HttpPost]
        public async Task<ActionResult<Device>> CreateDevice(DeviceDTO device)
        {
            bool DeviceExists = await context.Devices.AnyAsync(d => d.Id == device.Id);
            if (DeviceExists) return BadRequest("Device with the same ID already exists.");
            Device newDevice = new Device
            {
                Id = device.Id,
                Name = device.Name,
                Type = device.Type,
            };
            context.Devices.Add(newDevice);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDevice), new { id = device.Id }, newDevice);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevice(string id)
        {
            Device device = await context.Devices.FindAsync(id);
            if (device == null) return NotFound();
            context.Devices.Remove(device);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDevice(string id, DeviceDTO updatedDevice)
        {
            if (id != updatedDevice.Id) return BadRequest("ID mismatch.");
            Device device = await context.Devices.FindAsync(id);
            if (device == null) return NotFound();
            device.Name = updatedDevice.Name;
            device.Type = updatedDevice.Type;
            context.Devices.Update(device);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
