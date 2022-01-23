using System;
using FoundersPC.Application.Features.Hardware.Case.Models;
using FoundersPC.Application.Features.Hardware.HardDriveDisk.Models;
using FoundersPC.Application.Features.Hardware.Models;
using FoundersPC.Application.Features.Hardware.Motherboard.Models;
using FoundersPC.Application.Features.Hardware.PowerSupply.Models;
using FoundersPC.Application.Features.Hardware.Processor.Models;
using FoundersPC.Application.Features.Hardware.RandomAccessMemory.Models;
using FoundersPC.Application.Features.Hardware.SolidStateDrive.Models;
using FoundersPC.Application.Features.Hardware.VideoCard.Models;
using FoundersPC.Application.Features.Producer.Models;

namespace FoundersPC.UI.Admin.Locators;

public class SelectedObjectLocator
{
    private CaseInfo? _selectedCase;
    public CaseInfo? SelectedCase
    {
        get => _selectedCase;
        set
        {
            if (value == _selectedCase)
                return;

            _selectedCase = value;
            SelectedCaseChanged?.Invoke(value);
        }
    }
    public event Action<CaseInfo?>? SelectedCaseChanged;

    private HardDriveDiskInfo? _selectedHardDriveDisk;
    public HardDriveDiskInfo? SelectedHardDriveDisk
    {
        get => _selectedHardDriveDisk;
        set
        {
            if (value == _selectedHardDriveDisk)
                return;

            _selectedHardDriveDisk = value;
            SelectedHardDriveDiskChanged?.Invoke(value);
        }
    }
    public event Action<HardDriveDiskInfo?>? SelectedHardDriveDiskChanged;

    private MotherboardInfo? _selectedMotherboard;
    public MotherboardInfo? SelectedMotherboard
    {
        get => _selectedMotherboard;
        set
        {
            if (value == _selectedMotherboard)
                return;

            _selectedMotherboard = value;
            SelectedMotherboardChanged?.Invoke(value);
        }
    }
    public event Action<MotherboardInfo?>? SelectedMotherboardChanged;

    private PowerSupplyInfo? _selectedPowerSupply;
    public PowerSupplyInfo? SelectedPowerSupply
    {
        get => _selectedPowerSupply;
        set
        {
            if (value == _selectedPowerSupply)
                return;

            _selectedPowerSupply = value;
            SelectedPowerSupplyChanged?.Invoke(value);
        }
    }
    public event Action<PowerSupplyInfo?>? SelectedPowerSupplyChanged;

    private ProcessorInfo? _selectedProcessor;
    public ProcessorInfo? SelectedProcessor
    {
        get => _selectedProcessor;
        set
        {
            if (value == _selectedProcessor)
                return;

            _selectedProcessor = value;
            SelectedProcessorChanged?.Invoke(value);
        }
    }
    public event Action<ProcessorInfo?>? SelectedProcessorChanged;

    private RandomAccessMemoryInfo? _selectedRandomAccessMemory;
    public RandomAccessMemoryInfo? SelectedRandomAccessMemory
    {
        get => _selectedRandomAccessMemory;
        set
        {
            if (value == _selectedRandomAccessMemory)
                return;

            _selectedRandomAccessMemory = value;
            SelectedRandomAccessMemoryChanged?.Invoke(value);
        }
    }
    public event Action<RandomAccessMemoryInfo?>? SelectedRandomAccessMemoryChanged;

    private SolidStateDriveInfo? _selectedSolidStateDrive;
    public SolidStateDriveInfo? SelectedSolidStateDrive
    {
        get => _selectedSolidStateDrive;
        set
        {
            if (value == _selectedSolidStateDrive)
                return;

            _selectedSolidStateDrive = value;
            SelectedSolidStateDriveChanged?.Invoke(value);
        }
    }
    public event Action<SolidStateDriveInfo?>? SelectedSolidStateDriveChanged;

    private VideoCardInfo? _selectedVideoCard;
    public VideoCardInfo? SelectedVideoCard
    {
        get => _selectedVideoCard;
        set
        {
            if (value == _selectedVideoCard)
                return;

            _selectedVideoCard = value;
            SelectedVideoCardChanged?.Invoke(value);
        }
    }
    public event Action<VideoCardInfo?>? SelectedVideoCardChanged;

    private ProducerInfo? _selectedProducer;
    public ProducerInfo? SelectedProducer
    {
        get => _selectedProducer;
        set
        {
            if (value == _selectedProducer)
                return;

            _selectedProducer = value;
            SelectedProducerChanged?.Invoke(value);
        }
    }
    public event Action<ProducerInfo?>? SelectedProducerChanged;
}